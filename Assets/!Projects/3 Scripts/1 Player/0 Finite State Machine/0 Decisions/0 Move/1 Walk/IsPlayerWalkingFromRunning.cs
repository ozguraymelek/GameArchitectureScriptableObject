using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Player/Decision/Walk/to Run",fileName = "new Walk Data")]
    public class IsPlayerWalkingFromRunning : PlayerDecision
    {
        public Variable<Vector2> currentMovementInput;
        
        public override bool Decide(Controller.Player ctx)
        {
            var checkMovementInput = currentMovementInput.Value.x is > -.9f or > .9f 
                                     || currentMovementInput.Value.y != 0;

            return checkMovementInput;
        }
    }
}
