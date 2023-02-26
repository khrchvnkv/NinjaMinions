using System.Collections.Generic;
using NM.StaticData;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.Behaviour
{
    public interface IEnemy
    {
        void Construct(string id, EnemyStaticData enemyData, List<Vector3> patrolPoints);
    }
}