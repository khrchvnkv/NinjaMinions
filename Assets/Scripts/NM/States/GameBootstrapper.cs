using System.Collections.Generic;
using NM.LoadingView;
using NM.Services;
using UnityEngine;

namespace NM.States
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner, IUpdateRunner, IDontDestroyMarker
    {
        [SerializeField] private LoadingCurtain _curtain;

        private readonly List<IUpdater> _updaters = new List<IUpdater>();
        private readonly List<IFixedUpdater> _fixedUpdaters = new List<IFixedUpdater>();
        
        private Game _game;

        private void Awake()
        {
            var curtain = Instantiate(_curtain);
            _game = new Game(this, this, this, curtain, AllServices.Container);
            _game.StateMachine.Enter<BootstrapState>();  
            DontDestroyOnLoad(this);
        }
        private void Update() => _updaters.ForEach(u => u.UpdateLogic());
        private void FixedUpdate() => _fixedUpdaters.ForEach(u => u.FixedUpdateLogic());
        public void MarkAsDontDestroyable(GameObject destroyable) => DontDestroyOnLoad(destroyable);
        public void AddUpdate(IUpdater updater) => _updaters.Add(updater);
        public void RemoveUpdate(IUpdater updater) => _updaters.Remove(updater);
        public void AddFixedUpdate(IFixedUpdater fixedUpdater) => _fixedUpdaters.Add(fixedUpdater);
        public void RemoveFixedUpdate(IFixedUpdater fixedUpdater) => _fixedUpdaters.Remove(fixedUpdater);
    }
}
