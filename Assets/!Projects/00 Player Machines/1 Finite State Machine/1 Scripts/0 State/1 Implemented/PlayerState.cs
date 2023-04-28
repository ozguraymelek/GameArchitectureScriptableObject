using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Player/State",fileName = "new Player State")]
    public class PlayerState : PlayerBaseState
    {
        public List<PlayerAction> Actions = new List<PlayerAction>();
        public List<PlayerTransition> Transitions = new List<PlayerTransition>();

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