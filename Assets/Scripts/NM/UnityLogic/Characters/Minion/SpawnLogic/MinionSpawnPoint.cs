using NM.Services.Factory;
using NM.Services.Pool;
using UnityEngine;

namespace NM.UnityLogic.Characters.Minion.SpawnLogic
{
    public class MinionSpawnPoint : MonoBehaviour, IClearable, IPoolObject
    {
        private GameFactory _gameFactory;
        
        public void Construct(GameFactory gameFactory, string id)
        {
            _gameFactory = gameFactory;
            Spawn(id);
        }
        public void Clear() => _gameFactory.AddToPool<MinionSpawnPoint>(gameObject);
        private void Spawn(string id)
        {
            _gameFactory.CreateMinion(id, transform);
        }
    }
}