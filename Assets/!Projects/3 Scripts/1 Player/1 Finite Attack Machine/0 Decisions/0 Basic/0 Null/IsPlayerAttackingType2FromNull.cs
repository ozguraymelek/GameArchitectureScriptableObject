using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.FINITE_ATTACK_MACHINE
{
    [CreateAssetMenu(menuName = "Finite Attack Machine/Player/Decision/from Null/to Type 2",fileName = "new Null Decision Data")]
    public class IsPlayerAttackingType2FromNull : PlayerAttackDecision
    {
        public Variable<int> basicAttackValue;
        
        public Variable<bool> isPlayerAttacking;
        public Variable<bool> basicAttackButtonPressed;
        
        public override bool Decide(Controller.Player ctx)
        {
            return !isPlayerAttacking.Value && basicAttackButtonPressed.Value 
                                            && basicAttackValue.Value == 1;
        }
    }
}