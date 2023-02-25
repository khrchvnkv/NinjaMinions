using System;
using UnityEngine;

namespace NM.Services.Input
{
    public abstract class InputService : IService
    {
        public bool IsActive { get; private set; }
        public abstract Vector2 Axis { get; }
        public abstract bool IsChangeCharacterBtnPressed { get; }
        public event Action OnInputActivated;

        public void Activate()
        {
            IsActive = true;
            OnInputActivated?.Invoke();
        }
        public void Deactivate() => IsActive = false;
    }
}