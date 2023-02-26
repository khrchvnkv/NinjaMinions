using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Patrolman
{
    public class PatrolBehaviour : IEnemyBehaviour
    {
        private const float StopDistance = 0.1f;
        
        private readonly NavMeshAgent _agent;
        private readonly Transform _transform;
        private readonly List<Vector3> _patrolPoints;

        private int _currentPointIndex;
        
        public PatrolBehaviour(NavMeshAgent agent, Transform transform, List<Vector3> points)
        {
            _agent = agent;
            _transform = transform;
            _patrolPoints = new List<Vector3>(points);
        }
        public void Enter()
        {
            MoveToCurrentPoint();
        }
        public void UpdateBehaviour()
        {
            if (_patrolPoints.Count == 0) return;
            if (IsTargetReached())
            {
                SetNewTarget();
            }
        }
        public void Exit()
        {
        }
        private bool IsTargetReached()
        {
            if (_patrolPoints.Count <= 1) return false;

            var target = _patrolPoints[_currentPointIndex];
            return Vector3.Distance(_transform.position, target) <= StopDistance;
        }
        private void SetNewTarget()
        {
            _currentPointIndex++;
            if (_currentPointIndex >= _patrolPoints.Count) _currentPointIndex = 0;
            MoveToCurrentPoint();
        }
        private void MoveToCurrentPoint()
        {
            if (_patrolPoints.Count == 0) return;
            _agent.SetDestination(_patrolPoints[_currentPointIndex]);
        }
    }
}