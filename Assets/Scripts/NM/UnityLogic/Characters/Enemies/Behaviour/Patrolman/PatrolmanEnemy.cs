using System.Collections.Generic;
using NM.StaticData;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Patrolman
{
    public class PatrolmanEnemy : EnemyLogic<PatrolBehaviour>
    {
        public override void Construct(string id, EnemyStaticData enemyData, List<Vector3> patrolPoints)
        {
            base.Construct(id, enemyData, patrolPoints);
            IdleBehaviour = new PatrolBehaviour(Agent, transform, patrolPoints);
            EnterBehaviour(IdleBehaviour);
        }
        protected override void OnAggroZoneEntered(MinionContainer minion)
        {
            minion.MinionHp.TakeDamage(Damage);
            gameObject.SetActive(false);
            IsDied = true;
        }
    }
}