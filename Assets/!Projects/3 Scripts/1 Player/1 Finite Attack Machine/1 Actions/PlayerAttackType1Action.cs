using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.FINITE_ATTACK_MACHINE
{
    [CreateAssetMenu(menuName = "Finite Attack Machine/Player/Action/Basic Attacks/Type 1", fileName = "new Basic Type 1 Data")]
    public class PlayerAttackType1Action : PlayerAttackAction
    {
        public Variable<int> basicAttackValue;
        
        private static readonly int Property = Animator.StringToHash("Basic Attack");

        public override void Onset(Controller.Player ctx)
        {
            ctx.animator.SetBool(Property, true);
            
            CounterBasicAttackValue();
        }

        public override void Updating(Controller.Player ctx)
        {
            
        }
        
        private void CounterBasicAttackValue()
        {
            basicAttackValue.Value += 1;
        }
    }
}
