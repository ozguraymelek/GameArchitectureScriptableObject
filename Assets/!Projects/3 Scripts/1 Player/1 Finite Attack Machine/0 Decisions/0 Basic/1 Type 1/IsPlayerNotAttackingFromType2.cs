using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.FINITE_ATTACK_MACHINE
{
    [CreateAssetMenu(menuName = "Finite Attack Machine/Player/Decision/Basic Attacks/Type 2/to Null",fileName = "new Basic Type 2 Data")]
    public class IsPlayerNotAttackingFromType2 : PlayerAttackDecision
    {
        public Variable<bool> isPlayerNotAttacking;
        
        public override bool Decide(Controller.Player ctx)
        {
            return isPlayerNotAttacking.Value;
        }
    }
}