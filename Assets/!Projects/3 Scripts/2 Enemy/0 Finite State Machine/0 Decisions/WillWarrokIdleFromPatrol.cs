using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Decision/Idle/from Patrol",fileName = "new Idle from Patrol Decision Data")]
    public class WillWarrokIdleFromPatrol : EnemyDecision
    {
        public Variable<bool> isEnemyReached; 
        
        public override bool Decide(Controller.Enemy ctx)
        {
            return isEnemyReached.Value;
        }
    }
}