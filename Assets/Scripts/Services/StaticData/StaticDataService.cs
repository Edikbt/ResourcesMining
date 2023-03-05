using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ResourcesMining
{
    public class StaticDataService
    {
        private const string GameResourcesDataPath = "StaticData/GameResources";
        private const string SourcesDataPath = "StaticData/Sources";
        private const string SpotsDataPath = "StaticData/Spots";
        private const string LevelsDataPath = "StaticData/Levels";

        private Dictionary<ResourceTypeId, ResourceData> _resources;
        private Dictionary<SourceTypeId, SourceData> _sources;
        private Dictionary<SpotTypeId, SpotData> _spots;
        private Dictionary<string, LevelData> _levels;

        public StaticDataService()
        {
            Load();
        }

        public void Load()
        {
            _resources = Resources.LoadAll<ResourceData>(GameResourcesDataPath).ToDictionary(x => x.ResourceType, x => x);
            _sources = Resources.LoadAll<SourceData>(SourcesDataPath).ToDictionary(x => x.SourceType, x => x);
            _spots = Resources.LoadAll<SpotData>(SpotsDataPath).ToDictionary(x => x.SpotType, x => x);
            _levels = Resources.LoadAll<LevelData>(LevelsDataPath).ToDictionary(x => x.LevelName, x => x);
        }

        public ResourceData GetResourceData(ResourceTypeId resourceType) =>
            _resources.TryGetValue(resourceType, out ResourceData resource) ? resource : null;

        public SourceData GetSourceData(SourceTypeId sourceType) =>
            _sources.TryGetValue(sourceType, out SourceData source) ? source : null;

        public SpotData GetSpotData(SpotTypeId spotType) =>
            _spots.TryGetValue(spotType, out SpotData spot) ? spot : null;

        public LevelData GetLevelData(string levelName) =>
            _levels.TryGetValue(levelName, out LevelData level) ? level : null;
    }
}