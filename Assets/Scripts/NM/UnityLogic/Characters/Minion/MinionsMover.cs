using System.Collections.Generic;
using NM.Services;
using NM.Services.Factory;
using NM.Services.GameLoop;
using NM.Services.Input;
using NM.Services.PersistentProgress;
using NM.Services.UIWindows;
using NM.UnityLogic.UI;
using UnityEngine;

namespace NM.UnityLogic.Characters.Minion
{
    public class MinionsMover : MonoBehaviour, IClearable
    {
        private List<MinionContainer> _minions = new List<MinionContainer>();

        private GameLoopService _gameLoopService;
        private InputService _inputService;
        private WindowService _windowService;
        private PersistentProgressService _progressService;

        private CameraFollow _cameraFollow;
        private int _currentMinionIndex;

        public void Construct(InputService inputService, WindowService windowService, PersistentProgressService progressService)
        {
            _gameLoopService = AllServices.Container.Single<GameLoopService>();
            _inputService = inputService;
            _windowService = windowService;
            _progressService = progressService;
            _cameraFollow = Camera.main.GetComponent<CameraFollow>();
            _inputService.OnInputActivated += StartLogic;
        }
        public void AddMinion(MinionContainer minion) => _minions.Add(minion);
        public void Clear()
        {
            _inputService.OnInputActivated -= StartLogic;
            Destroy(gameObject);
        }
        private void Update()
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