using NM.Services.Input;
using NM.Services.PersistentProgress;
using NM.Services.StaticData;
using NM.Services.UIWindows;
using NM.StaticData;
using NM.UnityLogic.Characters.Minion;
using NM.UnityLogic.Characters.Minion.SpawnLogic;
using UnityEngine;

namespace NM.Services.Factory
{
    internal class MinionsFactory
    {
        private const string MinionsMover = "Characters/MinionsMover";
        private const string MinionSpawner = "Characters/Minions/MinionSpawnPoint";
        private const string CharactersMinion = "Characters/Minion";

        private readonly IUpdateRunner _updateRunner;
        private readonly AssetFactory _assetFactory;
        private readonly InputService _inputService;
        private readonly PersistentProgressService _progressService;
        private readonly StaticDataService _staticData;
        private readonly WindowService _windowService;
        private readonly GameFactory _gameFactory;

        private MinionsMover _mover;

        public MinionsFactory(IUpdateRunner updateRunner, AssetFactory assetFactory, InputService inputService, 
            PersistentProgressService progressService, StaticDataService staticData, WindowService windowService, 
            GameFactory gameFactory)
        {
            _updateRunner = updateRunner;
            _assetFactory = assetFactory;
            _inputService = inputService;
            _progressService = progressService;
            _staticData = staticData;
            _windowService = windowService;
            _gameFactory = gameFactory;
        }
        public MinionContainer GetMinionWithId(string minionId) => _mover.GetMinionWithId(minionId);
        public GameObject CreateMinionsMover()
        {
            var instance = _assetFactory.CreateAssetByName<MinionsMover>(MinionsMover);
            _mover = instance.GetComponent<MinionsMover>();
            _mover.Construct(_updateRunner, _inputService, _gameFactory, _windowService, _progressService);
            instance.SetActive(true);
            return instance;
        }
        public void CreateMinionSpawner(MinionSpawnerData spawnerData)
        {
            var spawner = _assetFactory.CreateAssetByName<MinionSpawnPoint>(MinionSpawner);
            GameFactory.MoveTo(spawner, spawnerData.SpawnPosition);
            var minionSpawner = spawner.GetComponent<MinionSpawnPoint>();
            minionSpawner.Construct(_gameFactory, spawnerData.Id);
        }
        public GameObject CreateMinion(string minionId, Transform parent)
        {
            var minion = _assetFactory.CreateAssetByName<MinionContainer>(CharactersMinion);
            GameFactory.MoveTransform(minion, parent);
            var minionStaticData = _staticData.MinionStaticData;
            var minionContainer = minion.GetComponent<MinionContainer>();
            _mover.AddMinion(minionContainer);
            minionContainer.Construct(_gameFactory, minionId, minionStaticData.MaxHp, minionStaticData.MovementSpeed);
            return minion;
        }
    }
}