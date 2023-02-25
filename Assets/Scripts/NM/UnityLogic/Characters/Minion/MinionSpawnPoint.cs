using NM.Data;
using NM.Services.Factory;
using UnityEngine;

namespace NM.UnityLogic.Characters.Minion
{
    public class MinionSpawnPoint : MonoBehaviour, IClearable
    {
        public string Id { get; private set; }

        private GameFactory _gameFactory;
        
        public void Construct(GameFactory gameFactory, string id)
        {
            _gameFactory = gameFactory;
            Id = id;
            
            // Check and spawn
            Spawn();
        }
        public void Clear() => Destroy(gameObject);
        private void Spawn()
        {
            _gameFactory.CreateMinion(Id, transform);
        }
    }
}