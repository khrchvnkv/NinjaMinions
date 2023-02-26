using System;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.UnityLogic.LevelExit
{
    public class MinionTriggerObserver : MonoBehaviour
    {
        public event Action<MinionContainer> OnMinionEntered;
        public event Action<MinionContainer> OnMinionExited;
        
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