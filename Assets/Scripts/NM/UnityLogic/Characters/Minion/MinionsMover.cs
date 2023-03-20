using System.Collections.Generic;
using NM.Services;
using NM.Services.Factory;
using NM.Services.GameLoop;
using NM.Services.Input;
using NM.Services.PersistentProgress;
using NM.Services.Pool;
using NM.Services.UIWindows;
using NM.UnityLogic.UI;
using UnityEngine;

namespace NM.UnityLogic.Characters.Minion
{
    public class MinionsMover : MonoBehaviour, IUpdater, IClearable, IPoolObject
    {
        private List<MinionContainer> _minions = new List<MinionContainer>();

        private IUpdateRunner _updateRunner;
        private GameLoopService _gameLoopService;
        private InputService _inputService;
        private GameFactory _gameFactory;
        private WindowService _windowService;
        private PersistentProgressService _progressService;

        private CameraFollow _cameraFollow;
        private int _currentMinionIndex;

        public void Construct(IUpdateRunner updateRunner, InputService inputService, GameFactory gameFactory, 
            WindowService windowService, PersistentProgressService progressService)
        {
            _updateRunner = updateRunner;
            _currentMinionIndex = 0;
            _gameLoopService = AllServices.Container.Single<GameLoopService>();
            _inputService = inputService;
            _gameFactory = gameFactory;
            _windowService = windowService;
            _progressService = progressService;
            _cameraFollow = Camera.main.GetComponent<CameraFollow>();
            updateRunner.AddUpdate(this);
            _inputService.OnInputActivated += StartLogic;
        }
        public MinionContainer GetMinionWithId(string id)
        {
            foreach (var minion in _minions)
            {
                if (minion.Id == id) return minion;
            }
            return null;
        }
        public void AddMinion(MinionContainer minion) => _minions.Add(minion);
        public void Clear()
        {
            _updateRunner.RemoveUpdate(this);
            _inputService.OnInputActivated -= StartLogic;
            _minions.Clear();
            _gameFactory.AddToPool<MinionsMover>(gameObject);
        }
        public void UpdateLogic()
        {
            if (!_inputService.IsActive) return;
            if (_minions.Count > 0)
            {
                MoveMinion();
                ChangeMinion();
            }
        }
        private void StartLogic()
        {
            UpdateHpTarget();
            UpdateCameraTarget();
        }
        private void MoveMinion()
        {
            if (_inputService.Axis.sqrMagnitude >= 0.025f)
            {
                // Move minion
                var currentMinion = _minions[_currentMinionIndex];
                currentMinion.MinionMove.Move(_inputService.Axis);
            }
        }
        private void ChangeMinion()
        {
            if (_inputService.IsChangeCharacterBtnPressed)
            {
                // Change Minion
                _currentMinionIndex++;
                if (_currentMinionIndex >= _minions.Count)
                {
                    _currentMinionIndex = 0;
                }

                UpdateHpTarget();
                UpdateCameraTarget();
            }
        }
        private void UpdateHpTarget()
        {
            var currentMinion = _minions[_currentMinionIndex];
            _windowService.GameHUD.Hide<HudWindowData>();
            _windowService.GameHUD.Show(new HudWindowData(_gameLoopService, _progressService, currentMinion.MinionHp));
        }
        private void UpdateCameraTarget()
        {
            _cameraFollow.SetTarget(_minions[_currentMinionIndex].transform);
        }
    }
}