using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Decision/Detect/from Attack",fileName = "new Detect from Attack Decision Data")]
    public class WillWarrokDetectFromAttack : EnemyDecision
    {
        public override bool Decide(Controller.Enemy ctx)
        {
            var check = ctx.attackableObjects.Length == 0;
            
            return check;
        }
    }
}