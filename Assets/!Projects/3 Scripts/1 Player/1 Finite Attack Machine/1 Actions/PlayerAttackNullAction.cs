using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UniRx;
using UnityEngine;

namespace Nacho.FINITE_ATTACK_MACHINE
{
    [CreateAssetMenu(menuName = "Finite Attack Machine/Player/Action/Null", fileName = "new Null Attack Data")]
    public class PlayerAttackNullAction : PlayerAttackAction
    {
        public Variable<int> basicAttackValue;

        private float _timer = 0;

        public override void Onset(Controller.Player ctx)
        {
            _timer = 0;
            
            ctx.ObserveEveryValueChanged(_ => _timer).Where(_ => _timer > 4f)
                .Subscribe(unit =>
            {
                ResetBasicAttackValue();
            });
            
            ctx.ObserveEveryValueChanged(_ => basicAttackValue.Value).Where(_ => basicAttackValue.Value >= 3)
                .Subscribe(unit =>
                {
                    basicAttackValue.Value = 0;
                });
        }

        public override void Updating(Controller.Player ctx)
        {
            _timer += Time.deltaTime;
        }

        private void ResetBasicAttackValue()
        {
            basicAttackValue.Value = 0;
            _timer = 0;
        }
    }
}