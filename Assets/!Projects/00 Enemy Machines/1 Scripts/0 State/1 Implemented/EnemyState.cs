using System.Collections;
using System.Collections.Generic;
using Nacho.Enemy.FINITE_STATE_MACHINE;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/State",fileName = "new Enemy State")]
    public class EnemyState : EnemyBaseState
    {
        public List<EnemyAction> Actions = new List<EnemyAction>();
        public List<EnemyTransition> Transitions = new List<EnemyTransition>();

        public override void Onset(Controller.Enemy ctx)
        {
            foreach (var action in Actions)
            {
                action.Onset(ctx);
            }
        }
        
        public override void Updating(Controller.Enemy ctx)
        {
            foreach (var action in Actions)
            {
                action.Updating(ctx);
            }
            
            foreach (var transition in Transitions)
            {
                if(Transitions.Count !=0)
                    transition.Execute(ctx);
            }
        }

        public override void OnDrawingGizmosSelected(Controller.Enemy ctx)
        {
            foreach (var action in Actions)
            {
                action.OnDrawingGizmosSelected(ctx);
            }
        }
    }
}