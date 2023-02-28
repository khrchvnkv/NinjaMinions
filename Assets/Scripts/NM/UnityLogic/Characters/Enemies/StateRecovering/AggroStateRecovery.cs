using System;
using NM.Services.Factory;
using NM.UnityLogic.Characters.Enemies.Behaviour;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.StateRecovering
{
    public class AggroStateRecovery : EnemyStateRecovery
    {
        [Serializable]
        private class StalkerState
        {
            public string MinionId;
            public bool IsAggro;
        }
        
        public AggroStateRecovery(Action<IEnemyBehaviour> enterBehaviourAction) : base(enterBehaviourAction) { }
        public string GenerateStateMeta(bool isAggro, IAggroBehaviour aggroBehaviour)
        {
            var state = new StalkerState();
            if (isAggro && aggroBehaviour != null)
            {
                state.MinionId = aggroBehaviour.MinionId;
                state.IsAggro = true;
            }
            return JsonUtility.ToJson(state);
        }
        public void RestoreBehaviourState(string stateMeta, IEnemyBehaviour idleBehaviour, 
            Action<MinionContainer> startAggroBehaviour, GameFactory gameFactory)
        {
            if (!string.IsNullOrEmpty(stateMeta))
            {
                var state = JsonUtility.FromJson<StalkerState>(stateMeta);
                if (state != null && state.IsAggro && !string.IsNullOrEmpty(state.MinionId))
                {
                    var minion = gameFactory.GetMinionWithId(state.MinionId);
                    if (minion != null)
                    {
                        startAggroBehaviour?.Invoke(minion);
                        return;
                    }
                }
            }
            EnterBehaviourAction?.Invoke(idleBehaviour);
        }
    }
}