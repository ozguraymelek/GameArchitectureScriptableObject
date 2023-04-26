using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using Nacho.Controller;
using UniRx;
using UnityEngine;

namespace Nacho.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Player/Action/Run", fileName = "new Run Data")]
    public class PlayerRunAction : PlayerAction
    {
        public Variable<Vector2> currentMovementInput;
        
        public Variable<float> appliedMovementX;
        public Variable<float> appliedMovementY;
        public Variable<float> appliedMovementZ;
        
        [SerializeField] private float smoothBlend;
        
        private static readonly int PropertyX = Animator.StringToHash("Pos X");
        private static readonly int PropertyZ = Animator.StringToHash("Pos Z");
        
        public override void Onset(Controller.Player ctx)
        {
            SetDirection(ctx);
        }

        public override void Updating(Controller.Player ctx)
        {
            Run(ctx);
        }

        private void Run(Controller.Player ctx)
        {
            ctx.animator.SetFloat(PropertyX, currentMovementInput.Value.x, smoothBlend, Time.fixedDeltaTime);
            ctx.animator.SetFloat(PropertyZ, currentMovementInput.Value.y, smoothBlend, Time.fixedDeltaTime);

            appliedMovementX.Value = currentMovementInput.Value.x * 5f;
            appliedMovementY.Value = 0f;
            appliedMovementZ.Value = currentMovementInput.Value.y * 5f;
            
            ctx.rb.velocity = new Vector3(appliedMovementX.Value, appliedMovementY.Value, appliedMovementZ.Value);
        }

        private void SetDirection(Controller.Player ctx)
        {
            ctx.ObserveEveryValueChanged(_ => currentMovementInput.Value)
                .Subscribe(
                    unit =>
                    {
                        if (currentMovementInput.Value.y > .9f)
                            ctx.playerCurrentDirection = PlayerDirection.ForwardRun;
                        
                        if (currentMovementInput.Value.x < -.9f)
                            ctx.playerCurrentDirection = PlayerDirection.LeftRun;
                        
                        else if (currentMovementInput.Value.x > .9f)
                            ctx.playerCurrentDirection = PlayerDirection.RightRun;
                    });
        }
    }
}