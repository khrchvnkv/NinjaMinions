using UnityEngine;

namespace NM.Services.Input
{
    public abstract class InputService : IService
    {
        public bool IsActive { get; private set; }
        public abstract Vector2 Axis { get; }
        public abstract bool IsChangeCharacterBtnPressed { get; }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}