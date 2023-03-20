using NM.Services.StaticData;
using NM.StaticData;
using NM.UnityLogic.Characters.Enemies;
using NM.UnityLogic.Characters.Enemies.Behaviour;
using NM.UnityLogic.Characters.Enemies.SpawnLogic;
using UnityEngine;

namespace NM.Services.Factory
{
    internal class EnemiesFactory
    {
        private const string EnemySpawner = "Characters/Enemies/EnemySpawnPoint";

        private readonly IUpdateRunner _updateRunner;
        private readonly AssetFactory _assetFactory;
        private readonly GameFactory _gameFactory;
        private readonly StaticDataService _staticData;

        public EnemiesFactory(IUpdateRunner updateRunner, AssetFactory assetFactory, GameFactory gameFactory, 
            StaticDataService staticData)
        {
            _updateRunner = updateRunner;
            _assetFactory = assetFactory;
            _gameFactory = gameFactory;
            _staticData = staticData;
        }
        public void CreateEnemySpawner(EnemySpawnerData spawnerData)
        {
            var spawner = _assetFactory.CreateAssetByName<EnemySpawnPoint>(EnemySpawner);
            GameFactory.MoveTransform(spawner, spawnerData.SpawnPosition, spawnerData.SpawnRotation);
            var enemySpawner = spawner.GetComponent<EnemySpawnPoint>();
            enemySpawner.Construct(_gameFactory, spawnerData);
        }
        public GameObject CreateEnemy(EnemySpawnerData spawnerData, Transform parent)
        {
            var enemyData = _staticData.GetEnemyData(spawnerData.EnemyTypeId);
            var enemy = _assetFactory.CreateEnemyAssetByData(enemyData, parent);
            GameFactory.MoveTransform(enemy, parent);
            var enemyConstruct = enemy.GetComponent<IEnemy>();
            enemyConstruct.Construct(_updateRunner, _gameFactory, spawnerData.Id, enemyData, spawnerData.Points);
            return enemy;
        }
        public GameObject CreateBullet(GameObject prefab, Transform parent, BulletLogic.BulletParams bulletParams)
        {
            var bullet = _assetFactory.CreateAssetByInstance<BulletLogic>(prefab, parent);
            GameFactory.MoveTransform(bullet, parent);
            bullet.GetComponent<BulletLogic>().Construct(_updateRunner, _gameFactory, bulletParams);
            return bullet;
        }
    }
}