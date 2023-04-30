using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Action/Detect", fileName = "new Detect Data")]
    public class WarrokDetectAction : EnemyAction
    {
        [Header("Settings /question mark")]
        public Variable<float> scaleQuestionMarkDelay;
        
        [Header("Settings /exclamation mark")]
        public Variable<float> scaleExclamationMarkDelay;
        public float shakeDelay;
        public float shakePosStrength;
        public int shakePosVibration;
        
        public override void Onset(Controller.Enemy ctx)
        {
            if (ctx.questionMark.activeSelf == true)
                UnscalingQuestionMark(ctx);

            if (ctx.detectedObjects.Length == 0)
            {
                // ReSharper disable once HeapView.ObjectAllocation.Evident
                ctx.detectedObjects = new Collider[1];
                ctx.detectedObjects[0] = ctx.suspicionObjects[0];
            }
            
            Array.Clear(ctx.suspicionObjects, 0, ctx.suspicionObjects.Length);
            
            ChangeTargetLayer(ctx);
        }

        public override void Updating(Controller.Enemy ctx)
        {
            if (ctx.exclamationMark.activeSelf == true)
            {
                ctx.exclamationMark.transform.Rotate(new Vector3(0f, 15f, 0f),
                    Space.Self);
            }
        }
        private void UnscalingQuestionMark(Controller.Enemy ctx)
        {
            var seq = DOTween.Sequence();
            
            seq.Append(ctx.questionMark.transform.DOScale(new Vector3(0f, 0f, 0f), scaleQuestionMarkDelay.Value));
            
            seq.AppendCallback(() =>
            {
                ctx.questionMark.SetActive(false);
                
                ScalingExclamationMark(ctx);
            });
        }
        
        private void ScalingExclamationMark(Controller.Enemy ctx)
        {
            var seq = DOTween.Sequence();
            
            ctx.exclamationMark.SetActive(true);

            seq.Append(ctx.exclamationMark.transform.DOScale(new Vector3(1f, 1f, 1f), scaleExclamationMarkDelay.Value));

            ctx.exclamationMark.transform.localScale = new Vector3(1f, 1f, 1f);
            
            seq.Append(
                    ctx.exclamationMark.transform.DOShakePosition(shakeDelay, shakePosStrength, 
                        shakePosVibration,180f))
                .SetLoops(-1);
        }
        
        private void ChangeTargetLayer(Controller.Enemy ctx)
        {
            ctx.detectedObjects[0].gameObject.layer = LayerMask.NameToLayer("Detected");
        }
    }
}