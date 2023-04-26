using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.FINITE_ATTACK_MACHINE
{
    [CreateAssetMenu(menuName = "Finite Attack Machine/Player/Decision/Basic Attacks/Type 3/to Null",fileName = "new Basic Type 3 Data")]
    public class IsPlayerAttackingType3FromNull : PlayerAttackDecision
    {
        public Variable<int> basicAttackValue;
        
        public Variable<bool> isPlayerAttacking;
        public Variable<bool> basicAttackButtonPressed;
        
        public override bool Decide(Controller.Player ctx)
        {
            return !isPlayerAttacking.Value && basicAttackButtonPressed.Value 
                                            && basicAttackValue.Value == 2;
        }
    }
}