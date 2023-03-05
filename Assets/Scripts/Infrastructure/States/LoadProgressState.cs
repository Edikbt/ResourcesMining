namespace ResourcesMining
{
    public class LoadProgressState : IState
    {
        private const string LevelName = "Level1";
        private readonly GameStateMachine _stateMachine;
        private readonly ProgressService _progressService;
        private readonly SaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, ProgressService progressService, SaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState, string>(LevelName);
        }        

        public void Exit() { }

        private void LoadProgressOrInitNew() => 
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
    }
}