using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using Nacho.Controller;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Nacho.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Player/Action/Idle/Normal", fileName = "new Normal Idle Data")]
    public class PlayerNormalIdleAction : PlayerAction
    {
        public Variable<Vector2> currentMovementInput;
        
        [SerializeField] private float smoothBlend;
        
        private static readonly int PropertyX = Animator.StringToHash("Pos X");
        private static readonly int PropertyZ = Animator.StringToHash("Pos Z");
        
        public override void Onset(Controller.Player ctx)
        {
            SetDirection(ctx);
        }

        public override void Updating(Controller.Player ctx)
        {
            Idle(ctx);
        }

        private void Idle(Controller.Player ctx)
        {
            ctx.animator.SetFloat(PropertyX, currentMovementInput.Value.x, smoothBlend, Time.fixedDeltaTime);
            ctx.animator.SetFloat(PropertyZ, currentMovementInput.Value.y, smoothBlend, Time.fixedDeltaTime);

            ctx.rb.velocity = Vector3.zero;
        }

        private void SetDirection(Controller.Player ctx)
        {
            ctx.playerCurrentDirection = PlayerDirection.Idle;
        }
    }
}