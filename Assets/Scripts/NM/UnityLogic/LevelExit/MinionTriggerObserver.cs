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

        public void ActivateTrigger() => _collider.enabled = true;
        public void DeactivateTrigger() => _collider.enabled = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MinionContainer minion))
            {
                OnMinionEntered?.Invoke(minion);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out MinionContainer minion))
            {
                OnMinionExited?.Invoke(minion);
            }
        }
    }
}