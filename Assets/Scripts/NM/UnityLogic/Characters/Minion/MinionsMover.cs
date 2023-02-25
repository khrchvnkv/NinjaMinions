using System.Collections.Generic;
using NM.Services.Factory;
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

        private GameStateMachine _gameStateMachine;
        private InputService _inputService;
        private WindowService _windowService;
        private PersistentProgressService _persistentProgressService;

        private CameraFollow _cameraFollow;
        private int _currentMinionIndex;

        public void Construct(GameStateMachine gameStateMachine, InputService inputService, 
            WindowService windowService, PersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _windowService = windowService;
            _persistentProgressService = persistentProgressService;
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
            _windowService.GameHUD.Show(new HudWindowData(_gameStateMachine, _inputService, _persistentProgressService, 
                currentMinion.MinionHp));
        }
        private void UpdateCameraTarget()
        {
            _cameraFollow.SetTarget(_minions[_currentMinionIndex].transform);
        }
    }
}