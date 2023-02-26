using NM.Services.Factory;
using UnityEngine;

namespace NM.UnityLogic.Characters.Minion.SpawnLogic
{
    public class MinionSpawnPoint : MonoBehaviour, IClearable
    {
        private GameFactory _gameFactory;
        
        public void Construct(GameFactory gameFactory, string id)
        {
            _gameFactory = gameFactory;
            
            // Check and spawn
            Spawn(id);
        }
        public void Clear() => Destroy(gameObject);
        private void Spawn(string id)
        {
            _gameFactory.CreateMinion(id, transform);
        }
    }
}