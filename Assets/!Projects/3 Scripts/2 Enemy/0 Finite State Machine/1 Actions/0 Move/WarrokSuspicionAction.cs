using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GenericScriptableArchitecture;
using Nacho.ObjectPools;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Action/Suspicion", fileName = "new Suspicion Data")]
    public class WarrokSuspicionAction : EnemyAction
    {
        public Variable<float> idleTimer; 
        
        [Header("Settings /question mark")]
        public float scaleDelay;
        public float shakeDelay;
        public float shakeScaleDelay;
        public int shakeScaleVibration;

        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsTurning = Animator.StringToHash("IsTurning");

        public override void Onset(Controller.Enemy ctx)
        {
            ctx.animator.SetBool(IsWalking, false);
            
            ctx.animator.SetTrigger(IsTurning);
            
            ctx.rb.velocity = Vector3.zero;
            
            ScalingQuestionMark(ctx);
            
            // ctx.suspicionObject = 
        }

        public override void Updating(Controller.Enemy ctx)
        {
            if (ctx.questionMark.activeSelf == true)
            {
                ctx.questionMark.transform.Rotate(new Vector3(0f, 5f, 0f),
                    Space.Self);
            }
        }

        private void ScalingQuestionMark(Controller.Enemy ctx)
        {
            var seq = DOTween.Sequence();
            
            ctx.questionMark.SetActive(true);

            seq.Append(ctx.questionMark.transform.DOScale(new Vector3(1f, 1f, 1f), scaleDelay));

            ctx.questionMark.transform.localScale = new Vector3(1f, 1f, 1f);
            
            seq.Append(
                    ctx.questionMark.transform.DOShakeScale(shakeDelay, shakeScaleDelay, shakeScaleVibration))
                .SetLoops(-1);
        }
    }
}