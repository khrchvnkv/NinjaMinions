using System;
using UnityEngine;

namespace NM.UnityLogic.UI
{
    public abstract class WindowBase<TData> : MonoBehaviour, IWindow where TData : class, IWindowData
    {
        protected GameHUD GameHUD;
        protected TData WindowData;

        public Type DataType => typeof(TData);

        public void Construct(GameHUD gameHUD) => GameHUD = gameHUD;
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
        void Construct(GameHUD gameHUD);
        void Show(IWindowData windowData);
        void Hide();
    }
}