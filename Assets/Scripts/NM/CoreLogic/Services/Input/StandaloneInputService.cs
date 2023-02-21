using UnityEngine;

namespace NM.CoreLogic.Services.Input
{
    public class StandaloneInputService : InputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        
        public override Vector2 Axis => 
            new Vector2(UnityEngine.Input.GetAxis(HorizontalAxis), UnityEngine.Input.GetAxis(VerticalAxis));
        public override bool IsChangeCharacterBtnPressed => UnityEngine.Input.GetKeyDown(KeyCode.Tab);
    }
}