using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Player/Action/Idle/Ninja", fileName = "new Ninja Idle Data")]
    public class PlayerNinjaIdleAction : PlayerAction
    {
        public Variable<Vector2> currentMovementInput;
        
        public override void Onset(Controller.Player ctx)
        {
            
        }

        public override void Updating(Controller.Player ctx)
        {
            
        }
    }
}