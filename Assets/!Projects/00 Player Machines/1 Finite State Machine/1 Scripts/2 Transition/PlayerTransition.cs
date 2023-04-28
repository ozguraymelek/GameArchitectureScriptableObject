using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Player/Transition")]
    public sealed class PlayerTransition : ScriptableObject
    {
        public PlayerDecision Decision;
        public PlayerBaseState NewState;
        public PlayerBaseState StayState;
        
        public void Execute(Controller.Player ctx)
        {
            if (Decision.Decide(ctx) && NewState is not PlayerRemainInState)
            {
                ctx.CurrentState = NewState;
            }
            
            else if (!Decision.Decide(ctx) && StayState is not PlayerRemainInState)
            {
                ctx.CurrentState = StayState;
            }
        }
    }
}