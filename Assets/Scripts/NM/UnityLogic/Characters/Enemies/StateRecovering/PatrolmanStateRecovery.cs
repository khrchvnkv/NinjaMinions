using System;
using NM.UnityLogic.Characters.Enemies.Behaviour;
using NM.UnityLogic.Characters.Enemies.Behaviour.Patrolman;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.StateRecovering
{
    public class PatrolmanStateRecovery : EnemyStateRecovery
    {
        [Serializable]
        private class PatrolmanState
        {
            public int PointIndex;
        }

        public PatrolmanStateRecovery(Action<IEnemyBehaviour> enterBehaviourAction) : base(enterBehaviourAction) { }
        public string GenerateStateMeta(PatrolBehaviour patrolBehaviour)
        {
            var state = new PatrolmanState();
            state.PointIndex = patrolBehaviour.CurrentPointIndex;
            return JsonUtility.ToJson(state);
        }
        public void RestoreBehaviourState(string stateMeta, PatrolBehaviour patrolBehaviour)
        {
            if (!string.IsNullOrEmpty(stateMeta))
            {
                var state = JsonUtility.FromJson<PatrolmanState>(stateMeta);
                if (state != null)
                {
                    patrolBehaviour.SetPointIndex(state.PointIndex);
                }
            }
            EnterBehaviourAction?.Invoke(patrolBehaviour);
        }
    }
}