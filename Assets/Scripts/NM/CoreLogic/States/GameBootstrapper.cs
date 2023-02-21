using NM.CoreLogic.LoadingView;
using NM.CoreLogic.Services;
using UnityEngine;

namespace NM.CoreLogic.States
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtain;
        
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, _curtain, AllServices.Container);
            _game.StateMachine.Enter<BootstrapState>();  
            DontDestroyOnLoad(this);
        }
    }
}
