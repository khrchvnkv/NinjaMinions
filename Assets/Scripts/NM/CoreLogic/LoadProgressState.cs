using NM.CoreLogic.Data;
using NM.CoreLogic.Services.PersistentProgress;
using NM.CoreLogic.Services.SaveLoad;
using NM.CoreLogic.States;

namespace NM.CoreLogic
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly PersistentProgressService _progressService;
        private readonly SaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, PersistentProgressService progressService,
            SaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        public void Enter()
        {
            LoadProgress();
            _gameStateMachine.Enter<LoadLevelState>(_progressService.Progress.LevelState.Level);
        }
        public void Exit()
        {
        }
        private void LoadProgress()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }
        private ProgressData NewProgress() => new ProgressData("Level_1");
    }
}