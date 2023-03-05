using System.Collections.Generic;
using UnityEngine;

namespace ResourcesMining
{
    public class GameFactory
    {
        public GameObject Character { get; private set; }
        public List<Source> Sources = new List<Source>();
        public List<Spot> Spots = new List<Spot>();

        private readonly AssetProvider _assetProvider;
        private readonly IInputService _inputService;
        private readonly StaticDataService _staticData;
        private readonly ProgressService _progressService;
        private readonly SaveLoadService _saveLoadService;

        public GameFactory(AssetProvider assetProvider, IInputService inputService, StaticDataService staticData, ProgressService progressService, SaveLoadService saveLoadService)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
            _staticData = staticData;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public GameObject CreateCharacter()
        {
            Character = _assetProvider.Instantiate(AssetAddress.HeroPath);
            Character.GetComponent<CharacterMove>().Construct(_inputService);
            Character.GetComponent<PickupResource>().Construct(_progressService, _saveLoadService);
            Character.GetComponent<Production>().Construct(this, _progressService, _inputService);
            return Character;
        }

        public GameObject CreateHud()
        {
            GameObject hud = _assetProvider.Instantiate(AssetAddress.HUDPath);
            hud.GetComponentInChildren<ResourceCounters>().Construct(_progressService);
            return hud;
        }

        public Source CreateSource(SourceTypeId sourceId, Vector3 position)
        {
            SourceData sourceData = _staticData.GetSourceData(sourceId);
            Source prefab = sourceData.SourcePrefab;
            Source source = GameObject.Instantiate(prefab, position, Quaternion.identity);

            source.Init(sourceData);
            source.GetComponent<Emitter>().Construct(this);
            Sources.Add(source);

            return source;
        }

        public Spot CreateSpot(SpotTypeId spotTypeId, Vector3 position)
        {
            SpotData spotData = _staticData.GetSpotData(spotTypeId);
            Spot prefab = spotData.SpotPrefab;
            Spot spot = GameObject.Instantiate(prefab, position, Quaternion.identity);

            spot.Init(spotData);
            spot.GetComponent<Emitter>().Construct(this);
            Spots.Add(spot);

            return spot;
        }

        public void CreateResourceFrom(ResourceTypeId resourceType, Vector3 position, int count)
        {
            ResourceData resourceData = _staticData.GetResourceData(resourceType);
            GameResource prefab = resourceData.ResourcePrefab;

            for (int i = 0; i < count; i++)
            {
                GameResource resource = GameObject.Instantiate(prefab, position, Quaternion.identity);
                resource.Init(resourceData);
                resource.StartImpulse(resourceData.Impulse);
                resource.StartPickUpDelay();
            }
        }

        public GameResource CreateResourceToSpot(ResourceTypeId resourceType, Vector3 position)
        {
            GameResource prefab = _staticData.GetResourceData(resourceType).ResourcePrefab;
            
            GameResource resource = GameObject.Instantiate(prefab, position, Quaternion.identity);
            resource.ResourceType = resourceType;

            return resource;
        }

        public void CreateTutorial(Source source, Absorber spotAbsorber)
        {
            GameObject tutorial = _assetProvider.Instantiate(AssetAddress.TutorialPath);
            tutorial.GetComponent<Tutorial>().Construct(source, spotAbsorber, _progressService);
        }
    }
}