using UnityEngine;

namespace NM.CoreLogic.Services.Input
{
    public abstract class InputService : IService
    {
        public bool IsActive { get; private set; }
        public abstract Vector2 Axis { get; }
        public abstract bool IsChangeCharacterBtnPressed { get; }

        public void Active() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}