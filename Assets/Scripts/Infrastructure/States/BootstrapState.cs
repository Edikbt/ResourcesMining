namespace ResourcesMining
{
    public class BootstrapState : IState
    {
        private const string StartSceneName = "StartScene";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() =>
            _sceneLoader.Load(StartSceneName, onLoaded: EnterLoadLevel);

        public void Exit() { }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadProgressState>();
    }
}