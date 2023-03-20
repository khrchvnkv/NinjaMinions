using NM.LoadingView;
using NM.Services;

namespace NM
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, IUpdateRunner updateRunner, IDontDestroyMarker dontDestroyMarker,
            LoadingCurtain loadingCurtain, AllServices services)
        {
            StateMachine = new GameStateMachine(coroutineRunner, updateRunner, dontDestroyMarker, new SceneLoader(coroutineRunner), loadingCurtain, services);
        }
    }
}
