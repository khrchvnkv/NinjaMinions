using NM.Data;
using NM.Services.PersistentProgress;
using NM.Services.SaveLoad;
using NM.States;

namespace NM
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
            var slot = _progressService.Progress.CurrentSlot;
            _gameStateMachine.Enter<LoadLevelState>(slot);
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