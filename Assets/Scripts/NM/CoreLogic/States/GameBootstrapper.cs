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
            var curtain = Instantiate(_curtain);
            _game = new Game(this, curtain, AllServices.Container);
            _game.StateMachine.Enter<BootstrapState>();  
            DontDestroyOnLoad(this);
        }
    }
}
