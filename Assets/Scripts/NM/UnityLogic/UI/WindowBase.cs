using System;
using NM.Services.UIWindows;
using UnityEngine;

namespace NM.UnityLogic.UI
{
    public abstract class WindowBase<TData> : MonoBehaviour, IWindow where TData : class, IWindowData
    {
        protected WindowService WindowService { get; private set; }
        protected TData WindowData { get; private set; }
        public Type DataType => typeof(TData);

        public void Construct(WindowService windowService) => WindowService = windowService;
        public virtual void Show(IWindowData windowData)
        {
            WindowData = windowData as TData;
            gameObject.SetActive(true);
        }
        public virtual void Hide()
        {
            WindowData = default;
            gameObject.SetActive(false);
        }
    }

    public interface IWindow
    {
        Type DataType { get; }
        void Construct(WindowService windowService);
        void Show(IWindowData windowData);
        void Hide();
    }
}