using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.FINITE_ATTACK_MACHINE
{
    [CreateAssetMenu(menuName = "Finite Attack Machine/Player/Transition")]
    public class PlayerAttackTransition : ScriptableObject
    {
        public PlayerAttackDecision Decision;
        public PlayerBaseAttackState NewState;
        public PlayerBaseAttackState StayState;

        public void Execute(Controller.Player ctx)
        {
            if (Decision.Decide(ctx) && NewState is not PlayerRemainInAttackState)
            {
                ctx.CurrentAttackState = NewState;
            }
            
            else if (!Decision.Decide(ctx) && StayState is not PlayerRemainInAttackState)
            {
                ctx.CurrentAttackState = StayState;
            }
        }
    }
}