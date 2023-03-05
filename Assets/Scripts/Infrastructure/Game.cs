namespace ResourcesMining
{
    public class Game
    {
        public GameStateMachine StateMachine;
        private SaveLoadService _saveLoadService;
        private GameFactory _gameFactory;
        private StaticDataService _staticDataService;
        private ProgressService _progressService;

        public Game(ICoroutineRunner coroutineRunner)
        {
            SceneLoader sceneLoader = new SceneLoader(coroutineRunner);
            RegisterServices();

            StateMachine = new GameStateMachine(sceneLoader, _gameFactory, _progressService, _saveLoadService, _staticDataService);
        }

        private void RegisterServices()
        {            
            IInputService inputService = new InputService();
            AssetProvider assetProvider = new AssetProvider();
            
            _staticDataService = new StaticDataService();
            _progressService = new ProgressService();
            _saveLoadService = new SaveLoadService(_progressService);
            _gameFactory = new GameFactory(assetProvider, inputService, _staticDataService, _progressService, _saveLoadService);
        }
    }
}