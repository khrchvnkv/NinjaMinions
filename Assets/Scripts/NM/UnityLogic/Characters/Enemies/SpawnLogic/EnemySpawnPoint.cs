using NM.Services.Factory;
using NM.StaticData;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.SpawnLogic
{
    public class EnemySpawnPoint : MonoBehaviour, IClearable
    {
        public void Construct(GameFactory gameFactory, EnemySpawnerData spawnerData)
        {
            Spawn(gameFactory, spawnerData);
        }
        public void Clear() => Destroy(gameObject);
        private void Spawn(GameFactory gameFactory, EnemySpawnerData spawnerData)
        {
            gameFactory.CreateEnemy(spawnerData, transform);
        }
    }
}