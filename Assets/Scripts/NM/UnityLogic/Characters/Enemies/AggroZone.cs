using System;
using NM.UnityLogic.Characters.Minion;
using NM.UnityLogic.LevelExit;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies
{
    public class AggroZone : MonoBehaviour
    {
        [SerializeField] private Transform _colliderParent;
        [SerializeField] private MinionTriggerObserver _minionTriggerObserver;

        public event Action<MinionContainer> OnAggroZoneEntered;
        public event Action<MinionContainer> OnAggroZoneExited;

        public void SetZoneScale(float scaleMultiplier) => _colliderParent.localScale = Vector3.one * scaleMultiplier;
        private void OnEnable()
        {
            _minionTriggerObserver.OnMinionEntered += AggroAction;
            _minionTriggerObserver.OnMinionExited += StopAggroAction;
        }
        private void OnDisable()
        {
            _minionTriggerObserver.OnMinionEntered -= AggroAction;
            _minionTriggerObserver.OnMinionExited -= StopAggroAction;
        }
        private void AggroAction(MinionContainer minion) => OnAggroZoneEntered?.Invoke(minion);
        private void StopAggroAction(MinionContainer minion) => OnAggroZoneExited?.Invoke(minion);
    }
}