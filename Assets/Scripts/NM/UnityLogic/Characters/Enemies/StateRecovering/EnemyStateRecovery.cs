using System;
using NM.UnityLogic.Characters.Enemies.Behaviour;

namespace NM.UnityLogic.Characters.Enemies.StateRecovering
{
    public abstract class EnemyStateRecovery
    {
        protected readonly Action<IEnemyBehaviour> EnterBehaviourAction;

        protected EnemyStateRecovery(Action<IEnemyBehaviour> enterBehaviourAction)
        {
            EnterBehaviourAction = enterBehaviourAction;
        }
    }
}