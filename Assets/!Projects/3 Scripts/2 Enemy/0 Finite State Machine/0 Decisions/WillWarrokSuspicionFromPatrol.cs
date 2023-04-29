using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Decision/Suspicion/from Patrol",fileName = "new Suspicion from Patrol Decision Data")]
    public class WillWarrokSuspicionFromPatrol : EnemyDecision
    {
        public Variable<bool> isSuspicion;
        
        public override bool Decide(Controller.Enemy ctx)
        {
            var check = ctx.suspicionObjects.Length != 0;
            
            return check;
        }
    }
}