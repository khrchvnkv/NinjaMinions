using System;
using System.Collections.Generic;
using UnityEngine;

namespace NM.UnityLogic.UI
{
    public class GameHUD : MonoBehaviour 
    {
        [SerializeField] private HudWindow hudWindow;
        [SerializeField] private SaveLoadWindow saveLoadWindow;

        private Dictionary<Type, IWindow> _windowsMap;

        public void Construct()
        {
            _windowsMap = new Dictionary<Type, IWindow>();
            AddWindow(hudWindow);
            AddWindow(saveLoadWindow);
            
            HideAllWindows();
            void AddWindow(IWindow window)
            {
                _windowsMap.Add(window.DataType, window);
                window.Construct(this);
            }
        }
        public void Show(IWindowData windowData)
        {
            var type = windowData.GetType();
            if (_windowsMap.TryGetValue(type, out var window))
            {
                window.Show(windowData);
            }
        }
        public void Hide<TData>() where TData : IWindowData
        {
            var type = typeof(TData);
            if (_windowsMap.TryGetValue(type, out var window))
            {
                window.Hide();
            }
        }
        private void HideAllWindows()
        {
            foreach (var window in _windowsMap.Values)
            {
                window.Hide();
            }
        }
    }
}