using NM.LoadingView;
using NM.Services;

namespace NM
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, IDontDestroyCreator dontDestroyCreator,
            LoadingCurtain loadingCurtain, AllServices services)
        {
            StateMachine = new GameStateMachine(coroutineRunner, dontDestroyCreator, new SceneLoader(coroutineRunner), loadingCurtain, services);
        }
    }
}
