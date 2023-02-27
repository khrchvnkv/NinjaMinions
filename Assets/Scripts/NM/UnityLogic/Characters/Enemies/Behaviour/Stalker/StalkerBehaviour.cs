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
        private readonly AggroZone _aggroZone;
        private readonly Action<MinionContainer> _attackAction;
        
        public StalkerBehaviour(NavMeshAgent agent, Transform target, 
            AggroZone aggroZone, Action<MinionContainer> attackAction)
        {
            _agent = agent;
            _target = target;
            _aggroZone = aggroZone;
            _attackAction = attackAction;
        }
        public void Enter()
        {
            _aggroZone.OnAggroZoneEntered += _attackAction;
        }
        public void UpdateBehaviour()
        {
            _agent.SetDestination(_target.position);
        }
        public void Exit()
        {
            _aggroZone.OnAggroZoneEntered -= _attackAction;
        }
    }
}