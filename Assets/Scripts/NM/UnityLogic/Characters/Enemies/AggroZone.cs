using System;
using NM.UnityLogic.Characters.Minion;
using NM.UnityLogic.LevelExit;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(MinionTriggerObserver))]
    public class AggroZone : MonoBehaviour
    {
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private MinionTriggerObserver _minionTriggerObserver;

        public event Action<MinionContainer> OnAggroZoneEntered;

        public void SetZoneRadius(float radius) => _collider.radius = radius;
        private void OnEnable()
        {
            _minionTriggerObserver.OnMinionEntered += AggroAction;
        }
        private void OnDisable()
        {
            _minionTriggerObserver.OnMinionEntered -= AggroAction;
        }
        private void AggroAction(MinionContainer minion) => OnAggroZoneEntered?.Invoke(minion);
    }
}