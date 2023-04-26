using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.FINITE_ATTACK_MACHINE
{
    [CreateAssetMenu(menuName = "Finite Attack Machine/Player/State",fileName = "new Player Attack State")]
    public class PlayerAttackState : PlayerBaseAttackState
    {
        public List<PlayerAttackAction> Actions = new List<PlayerAttackAction>();
        public List<PlayerAttackTransition> Transitions = new List<PlayerAttackTransition>();

        public override void Onset(Controller.Player ctx)
        {
            foreach (var action in Actions)
            {
                action.Onset(ctx);
            }
        }
        
        public override void Updating(Controller.Player ctx)
        {
            foreach (var action in Actions)
            {
                action.Updating(ctx);
            }
            
            foreach (var transition in Transitions)
            {
                transition.Execute(ctx);
            }
        }
    }
}