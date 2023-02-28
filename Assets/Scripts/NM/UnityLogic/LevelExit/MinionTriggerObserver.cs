using System;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.UnityLogic.LevelExit
{
    public class MinionTriggerObserver : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        public event Action<MinionContainer> OnMinionEntered;
        public event Action<MinionContainer> OnMinionExited;

        public void ActivateTrigger() => _collider.gameObject.SetActive(true);
        public void DeactivateTrigger() => _collider.gameObject.SetActive(false);
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MinionContainer minion))
            {
                if (IsCorrectTriggerAction(other))
                {
                    OnMinionEntered?.Invoke(minion);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out MinionContainer minion))
            {
                OnMinionExited?.Invoke(minion);
            }
        }
        private bool IsCorrectTriggerAction(Collider other)
        {
            var colliderTransform = _collider.transform;
            var minionTransform = other.transform;
            return Physics.ComputePenetration(_collider, colliderTransform.position,
                colliderTransform.rotation, other, minionTransform.position,
                minionTransform.rotation, out var _, out var __);
        }
    }
}