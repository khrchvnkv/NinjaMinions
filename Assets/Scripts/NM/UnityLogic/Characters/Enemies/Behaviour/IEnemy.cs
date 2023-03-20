using System.Collections.Generic;
using NM.Services.Factory;
using NM.StaticData;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.Behaviour
{
    public interface IEnemy
    {
        void Construct(IUpdateRunner updateRunner, GameFactory gameFactory, string id, 
            EnemyStaticData enemyData, List<Vector3> patrolPoints);
    }
}