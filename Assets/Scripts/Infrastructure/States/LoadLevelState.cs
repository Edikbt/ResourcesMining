using UnityEngine;
using UnityEngine.SceneManagement;

namespace ResourcesMining
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;        
        private readonly GameFactory _gameFactory;
        private readonly StaticDataService _staticData;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, GameFactory gameFactory, StaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() { }

        private void OnLoaded()
        {
            CreateCharacter();
            InitWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private void CreateCharacter()
        {
            GameObject character = _gameFactory.CreateCharacter();
            _gameFactory.CreateHud();
            CameraFollow(character);
        }

        private void InitWorld()
        {
            LevelData levelData = _staticData.GetLevelData(SceneManager.GetActiveScene().name);
            CreateSources(levelData);
            CreaateSpots(levelData);
            CreateTutorial();
        }

        private void CreateSources(LevelData levelData)
        {
            foreach (SourcePosition source in levelData.Sources)
                _gameFactory.CreateSource(source.SourceType, source.Position);
        }
        private void CreaateSpots(LevelData levelData)
        {
            foreach (SpotPosition spot in levelData.Spots)
                _gameFactory.CreateSpot(spot.SpotType, spot.Position);
        }

        private void CreateTutorial() =>
            _gameFactory.CreateTutorial(_gameFactory.Sources[0], _gameFactory.Spots[0].GetComponent<Absorber>());

        private void CameraFollow(GameObject hero) => 
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}
