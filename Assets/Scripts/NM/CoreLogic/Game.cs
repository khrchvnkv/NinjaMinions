using NM.CoreLogic.LoadingView;
using NM.CoreLogic.Services;

namespace NM.CoreLogic
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, AllServices services)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, services);
        }
    }
}
