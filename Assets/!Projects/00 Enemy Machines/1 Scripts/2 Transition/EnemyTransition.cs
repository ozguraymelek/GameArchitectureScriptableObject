using System.Collections;
using System.Collections.Generic;
using Nacho.FINITE_STATE_MACHINE;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Transition")]
    public sealed class EnemyTransition : ScriptableObject
    {
        public EnemyDecision Decision;
        public EnemyBaseState NewState;
        public EnemyBaseState StayState;
        
        public void Execute(Controller.Enemy ctx)
        {
            if (Decision.Decide(ctx) && NewState is not EnemyRemainInState)
            {
                ctx.CurrentState = NewState;
            }
            
            else if (!Decision.Decide(ctx) && StayState is not EnemyRemainInState)
            {
                ctx.CurrentState = StayState;
            }
        }
    }
}