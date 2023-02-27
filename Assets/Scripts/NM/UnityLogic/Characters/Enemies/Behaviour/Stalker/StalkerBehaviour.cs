using System;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;
using UnityEngine.AI;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Stalker
{
    internal class StalkerBehaviour : IEnemyBehaviour
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform _target;
        
        public StalkerBehaviour(NavMeshAgent agent, Transform target)
        {
            _agent = agent;
            _target = target;
        }
        public void Enter()
        {
        }
        public void UpdateBehaviour()
        {
            _agent.SetDestination(_target.position);
        }
        public void Exit()
        {
        }
    }
}