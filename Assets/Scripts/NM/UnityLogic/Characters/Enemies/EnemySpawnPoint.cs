using NM.Data;
using NM.Services.Factory;
using NM.Services.PersistentProgress;
using NM.StaticData;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies
{
    public class EnemySpawnPoint : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private EnemyStaticData.EnemyTypeId _enemyType;

        public string Id { get; private set; }

        private GameFactory _gameFactory;
        
        public void Construct(GameFactory gameFactory, string id, EnemyStaticData.EnemyTypeId enemyType)
        {
            _gameFactory = gameFactory;
            Id = id;
            _enemyType = enemyType;
            
            // Check and spawn
        }
        public void SaveProgress(SaveSlotData slot)
        {
            
        }
        public void LoadProgress(SaveSlotData slot)
        {
            
        }
        private void Spawn()
        {
            _gameFactory.CreateEnemy(_enemyType, transform);
        }
    }
}