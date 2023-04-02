using System;
using System.Collections.Generic;
using NM.Services.UIWindows;
using UnityEngine;

namespace NM.UnityLogic.UI
{
    public class GameHUD : MonoBehaviour 
    {
        [SerializeField] private HudWindow hudWindow;
        [SerializeField] private SaveLoadWindow saveLoadWindow;

        private Dictionary<Type, IWindow> _windowsMap;

        public void Construct(WindowService windowService)
        {
            _windowsMap = new Dictionary<Type, IWindow>();
            
            AddWindow(hudWindow);
            AddWindow(saveLoadWindow);
            
            HideAllWindows();
            void AddWindow(IWindow window)
            {
                _windowsMap.Add(window.DataType, window);
                window.Construct(windowService);
            }
        }
        public void Show<TData>(TData windowData) where TData : IWindowData
        {
            if (_windowsMap.TryGetValue(typeof(TData), out var window))
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