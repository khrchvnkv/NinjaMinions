using NM.Services.Factory;
using NM.Services.Pool;
using NM.StaticData;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.SpawnLogic
{
    public class EnemySpawnPoint : MonoBehaviour, IClearable, IPoolObject
    {
        private GameFactory _gameFactory;
        
        public void Construct(GameFactory gameFactory, EnemySpawnerData spawnerData)
        {
            _gameFactory = gameFactory;
            Spawn(gameFactory, spawnerData);
        }
        public void Clear() => _gameFactory.AddToPool<EnemySpawnPoint>(gameObject);
        private void Spawn(GameFactory gameFactory, EnemySpawnerData spawnerData)
        {
            gameFactory.CreateEnemy(spawnerData, transform);
        }
    }
}