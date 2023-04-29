using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using Nacho.Enemy.FINITE_STATE_MACHINE;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Decision/Patrol/from Idle",fileName = "new Patrol from Idle Decision Data")]
    public class WillEnemyPatrolFromIdle : EnemyDecision
    {
        public Variable<float> idleTimer;

        public Variable<float> delay;
        
        public override bool Decide(Controller.Enemy ctx)
        {
            var check = idleTimer.Value >= delay.Value;
            
            return check;
        }
    }
}