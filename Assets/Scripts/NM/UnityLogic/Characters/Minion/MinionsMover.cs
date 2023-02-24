using System.Collections.Generic;
using NM.Services.Input;
using UnityEngine;

namespace NM.UnityLogic.Characters.Minion
{
    public class MinionsMover : MonoBehaviour
    {
        private List<MinionMove> _minions = new List<MinionMove>();

        private InputService _inputService;
        private int _currentMinionIndex;

        public void Construct(InputService inputService)
        {
            _inputService = inputService;
        }
        public void AddMinion(MinionMove minion)
        {
            _minions.Add(minion);
        }
        private void Update()
        {
            if (!_inputService.IsActive) return;
            if (_minions.Count > 0)
            {
                if (_inputService.Axis.sqrMagnitude >= 0.025f)
                {
                    // Move minion
                    var currentMinion = _minions[_currentMinionIndex];
                    currentMinion.Move(_inputService.Axis);
                }

                if (_inputService.IsChangeCharacterBtnPressed)
                {
                    // Change Minion
                    _currentMinionIndex++;
                    if (_currentMinionIndex >= _minions.Count)
                    {
                        _currentMinionIndex = 0;
                    }
                }
            }
        }
    }
}