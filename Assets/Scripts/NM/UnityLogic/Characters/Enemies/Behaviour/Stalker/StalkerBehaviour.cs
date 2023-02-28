using System;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;
using UnityEngine.AI;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Stalker
{
    internal class StalkerBehaviour : IAggroBehaviour
    {
        private readonly NavMeshAgent _agent;
        private readonly MinionContainer _target;
        private readonly Transform _targetTransform;

        public string MinionId => _target.Id;
        
        public StalkerBehaviour(NavMeshAgent agent, MinionContainer minion)
        {
            _agent = agent;
            _target = minion;
            _targetTransform = minion.transform;
        }
        public void Enter()
        {
        }
        public void UpdateBehaviour()
        {
            _agent.SetDestination(_targetTransform.position);
        }
        public void Exit()
        {
        }
    }
}