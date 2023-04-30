using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Decision/Attack/from Detect",fileName = "new Attack from Suspicion Decision Data")]
    public class WillWarrokAttackFromDetect : EnemyDecision
    {
        public override bool Decide(Controller.Enemy ctx)
        {
            var check = ctx.attackableObjects.Length != 0;
            
            return check;
        }
    }
}
