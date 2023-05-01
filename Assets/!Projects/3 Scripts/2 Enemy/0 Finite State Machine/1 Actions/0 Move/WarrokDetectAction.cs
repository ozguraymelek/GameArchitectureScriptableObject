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
        [Header("Settings /detect")]
        public Variable<float> attackRadius;
        public LayerMask attackLayer;
        
        [Header("Settings /detect")]
        public Variable<float> detectRadius;
        public LayerMask detectLayer;
        public Variable<float> detectableTimer;
        
        [Header("Settings /lock")]
        private Vector3 _targetDirection;
        private Vector3 _enemyCalculateVector;
        private float _currentEnemyEulerY;
        private float _angle;
        
        [Header("Settings /question mark")]
        public Variable<float> scaleQuestionMarkDelay;
        
        [Header("Settings /exclamation mark")]
        public Variable<float> scaleExclamationMarkDelay;
        public float shakeDelay;
        public float shakePosStrength;
        public int shakePosVibration;
        
        [Header("Settings /animation keywords")]
        private static readonly int IsDetected = Animator.StringToHash("IsDetected");
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        
        public override void Onset(Controller.Enemy ctx)
        {
            detectableTimer.Value = 0;
            
            if (ctx.questionMark.activeSelf == true)
                UnscalingQuestionMark(ctx);
            
            ChangeTargetLayer(ctx);
            
            ctx.animator.SetBool(IsAttacking, false);
            ctx.animator.SetBool(IsDetected, true);
        }

        public override void Updating(Controller.Enemy ctx)
        {
            AttackRaycast(ctx);
            
            if (ctx.exclamationMark.activeSelf == true)
            {
                ctx.exclamationMark.transform.Rotate(new Vector3(0f, 15f, 0f),
                    Space.Self);
            }
            
            CalculateAngle(ctx);
            
            Move(ctx);
        }

        protected override void AttackRaycast(Controller.Enemy ctx)
        {
            ctx.attackableObjects = Physics.OverlapSphere(
                ctx.transform.position + new Vector3(0f, ctx.transform.localScale.y, 0f),
                attackRadius.Value, attackLayer);
        }
        
        private void Move(Controller.Enemy ctx)
        {
            if(ctx.detectedObjects.Length == 0)
                ctx.rb.velocity = _targetDirection * (8.5f * Time.fixedDeltaTime);
            
            else if(ctx.detectedObjects.Length != 0)
                ctx.rb.velocity = _targetDirection * (12.5f * Time.fixedDeltaTime);
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
            ctx.activePlayer.gameObject.layer = LayerMask.NameToLayer("Detected");
        }
        
        private void LockedToTarget(Controller.Enemy ctx)
        {
            var targetRot = Quaternion.Euler(0f, _currentEnemyEulerY - _angle, 0f);

            ctx.transform.rotation = Quaternion.Slerp(ctx.transform.rotation, targetRot, 
                1.5f * Time.deltaTime);
        }
        
        private void CalculateAngle(Controller.Enemy ctx)
        {
            _targetDirection = ctx.activePlayer.transform.position - ctx.transform.position;

            _enemyCalculateVector = ctx.transform.forward;

            _currentEnemyEulerY = ctx.transform.localEulerAngles.y - 360f;

            _angle = Vector3.SignedAngle(_targetDirection, _enemyCalculateVector, Vector3.up);

            LockedToTarget(ctx);
        }

        public override void OnDrawingGizmosSelected(Controller.Enemy ctx)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(ctx.transform.position + new Vector3(0f, ctx.transform.localScale.y, 0f),
                attackRadius.Value);
        }
    }
}