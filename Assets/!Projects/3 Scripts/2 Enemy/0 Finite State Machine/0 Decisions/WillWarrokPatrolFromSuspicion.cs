using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Decision/Patrol/from Suspicion",fileName = "new Patrol from Suspicion Decision Data")]
    public class WillWarrokPatrolFromSuspicion : EnemyDecision
    {
        public override bool Decide(Controller.Enemy ctx)
        {
            var check = ctx.suspicionObjects.Length == 0;
            
            return check;
        }
    }
}