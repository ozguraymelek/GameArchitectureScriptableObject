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
        [Header("Settings /lock")]
        private Vector3 _targetDirection;
        private Vector3 _enemyCalculateVector;
        private float _currentEnemyEulerY;
        private float _angle;
        
        [Header("Settings /suspicion")]
        public Variable<float> suspicionRadius;
        public LayerMask suspicionLayer;

        [Header("Settings /detect")]
        public Variable<float> detectRadius;
        public LayerMask detectLayer;
        public Variable<float> detectableTimer;
        
        [Header("Settings /question mark")]
        public Variable<float> scaleQuestionMarkDelay;
        public float shakeDelay;
        public float shakeScaleStrength;
        public int shakeScaleVibration;

        [Header("Settings /animation keywords")]
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int SuspicionValue = Animator.StringToHash("Suspicion Value");

        public override void Onset(Controller.Enemy ctx)
        {
            detectableTimer.Value = 0;
            
            ctx.animator.SetBool(IsWalking, false);
            
            ctx.activePlayer = ctx.suspicionObjects[0].GetComponent<Controller.Player>();
            
            ctx.rb.velocity = Vector3.zero;
            
            ScalingQuestionMark(ctx);
            
            CalculateAngle(ctx);

            ChangeTargetLayer(ctx);
        }

        public override void Updating(Controller.Enemy ctx)
        {
            SuspicionRaycast(ctx);
            
            if (ctx.questionMark.activeSelf == true)
            {
                ctx.questionMark.transform.Rotate(new Vector3(0f, 5f, 0f),
                    Space.Self);
            }

            ctx.animator.SetInteger(SuspicionValue, ctx.suspicionObjects.Length);
            
            LockedToTarget(ctx);

            DetectCounter();
        }
        
        protected override void SuspicionRaycast(Controller.Enemy ctx)
        {
            ctx.suspicionObjects = Physics
                .OverlapSphere(ctx.transform.position
                               + new Vector3(0f, ctx.transform.localScale.y, 0f),
                    suspicionRadius.Value,suspicionLayer);
        }
        
        private void ScalingQuestionMark(Controller.Enemy ctx)
        {
            var seq = DOTween.Sequence();
            
            ctx.questionMark.SetActive(true);

            seq.Append(ctx.questionMark.transform.DOScale(new Vector3(1f, 1f, 1f), scaleQuestionMarkDelay.Value));

            ctx.questionMark.transform.localScale = new Vector3(1f, 1f, 1f);
            
            seq.Append(
                    ctx.questionMark.transform.DOShakeScale(shakeDelay, shakeScaleStrength, 
                        shakeScaleVibration))
                .SetLoops(-1);
        }

        private void LockedToTarget(Controller.Enemy ctx)
        {
            var targetRot = Quaternion.Euler(0f, _currentEnemyEulerY - _angle, 0f);

            ctx.transform.rotation = Quaternion.Slerp(ctx.transform.rotation, targetRot, 
                .75f * Time.deltaTime);
        }
        
        private void CalculateAngle(Controller.Enemy ctx)
        {
            _targetDirection = ctx.suspicionObjects[0].transform.position - ctx.transform.position;

            _enemyCalculateVector = ctx.transform.forward;

            _currentEnemyEulerY = ctx.transform.localEulerAngles.y - 360f;

            _angle = Vector3.SignedAngle(_targetDirection, _enemyCalculateVector, Vector3.up);
        }

        private void DetectCounter()
        {
            detectableTimer.Value += Time.deltaTime;
        }

        private void ChangeTargetLayer(Controller.Enemy ctx)
        {
            ctx.activePlayer.gameObject.layer = LayerMask.NameToLayer("Suspected");
        }
        
        public override void OnDrawingGizmosSelected(Controller.Enemy ctx)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(ctx.transform.position + new Vector3(0f, ctx.transform.localScale.y, 0f),
                suspicionRadius.Value);
        }
    }
}