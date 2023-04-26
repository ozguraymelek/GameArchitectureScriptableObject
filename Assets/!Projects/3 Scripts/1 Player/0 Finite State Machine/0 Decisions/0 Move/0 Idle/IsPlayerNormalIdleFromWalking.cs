using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Player/Decision/Idle/Normal Idle",fileName = "new Normal Idle Data")]
    public class IsPlayerNormalIdleFromWalking : PlayerDecision
    {
        public Variable<Vector2> currentMovementInput;
        
        public override bool Decide(Controller.Player ctx)
        {
            var checkMovementInput = currentMovementInput.Value.x == 0 && currentMovementInput.Value.y == 0;

            return checkMovementInput;
        }
    }
}
