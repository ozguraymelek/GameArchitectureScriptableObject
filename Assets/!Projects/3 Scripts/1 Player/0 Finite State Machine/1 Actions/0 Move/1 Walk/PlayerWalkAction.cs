using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using Nacho.Controller;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Nacho.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Player/Action/Walk", fileName = "new Walk Data")]
    public class PlayerWalkAction : PlayerAction
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
            Move(ctx);
            Look(ctx);
        }

        private void Move(Controller.Player ctx)
        {
            ctx.animator.SetFloat(PropertyX, currentMovementInput.Value.x, smoothBlend, Time.fixedDeltaTime);
            ctx.animator.SetFloat(PropertyZ, currentMovementInput.Value.y, smoothBlend, Time.fixedDeltaTime);
            
            appliedMovementX.Value = currentMovementInput.Value.x * 1.2f;
            appliedMovementY.Value = 0f;
            appliedMovementZ.Value = currentMovementInput.Value.y * 1.2f;
            
            ctx.rb.velocity = new Vector3(appliedMovementX.Value, appliedMovementY.Value, appliedMovementZ.Value);
        }

        private void Look(Controller.Player ctx)
        {
            if (ctx.playerCurrentDirection != PlayerDirection.ForwardWalk) return;
            
            var targetRot = Quaternion.Euler(0, ctx.playerVirtualCamera.transform.eulerAngles.y
                , 0);

            ctx.transform.rotation = Quaternion.Slerp(ctx.transform.rotation, targetRot, 70f * Time.deltaTime);
        }
        
        private void SetDirection(Controller.Player ctx)
        {
            ctx.ObserveEveryValueChanged(_ => currentMovementInput.Value)
                .Subscribe(
                    unit =>
                    {
                        ctx.playerCurrentDirection = currentMovementInput.Value.x switch
                        {
                            < .1f and > -.1f when currentMovementInput.Value.y > 0 => PlayerDirection.ForwardWalk,
                            < .1f and > -.1f when currentMovementInput.Value.y < 0 => PlayerDirection.BackwardWalk,
                            
                            _ => ctx.playerCurrentDirection
                        };
                        
                        ctx.playerCurrentDirection = currentMovementInput.Value.y switch
                        {
                            < .1f and > -.1f when currentMovementInput.Value.x > 0 => PlayerDirection.RightWalk,
                            < .1f and > -.1f when currentMovementInput.Value.x < 0 => PlayerDirection.LeftWalk,
                            
                            _ => ctx.playerCurrentDirection
                        };
                        
                        ctx.playerCurrentDirection = currentMovementInput.Value.x switch
                        {
                            < 0 and > -.5f when currentMovementInput.Value.y < 0 => PlayerDirection.BackwardLeftWalk,
                            > 0 and > .5f when currentMovementInput.Value.y < 0 => PlayerDirection.BackwardRightWalk,
                            
                            _ => ctx.playerCurrentDirection
                        };
                    });
        }
    }
}