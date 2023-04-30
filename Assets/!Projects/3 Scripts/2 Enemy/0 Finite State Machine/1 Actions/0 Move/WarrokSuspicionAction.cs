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

        [Header("Settings /detect")] 
        public Variable<float> detectableTimer;

        [Header("Settings /question mark")]
        public float scaleDelay;
        public float shakeDelay;
        public float shakeScaleDelay;
        public int shakeScaleVibration;

        [Header("Settings /animation keyword")]
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsTurning = Animator.StringToHash("IsTurning");
        private static readonly int SuspicionValue = Animator.StringToHash("Suspicion Value");

        public override void Onset(Controller.Enemy ctx)
        {
            ctx.animator.SetBool(IsWalking, false);
            // ctx.animator.SetBool(IsTurning, true);
            
            ctx.rb.velocity = Vector3.zero;
            
            ScalingQuestionMark(ctx);
            
            CalculateAngle(ctx);
        }

        public override void Updating(Controller.Enemy ctx)
        {
            if (ctx.questionMark.activeSelf == true)
            {
                ctx.questionMark.transform.Rotate(new Vector3(0f, 5f, 0f),
                    Space.Self);
            }

            ctx.animator.SetInteger(SuspicionValue, ctx.suspicionObjects.Length);
            
            LockedToTarget(ctx);

            Detectable();
        }

        private void ScalingQuestionMark(Controller.Enemy ctx)
        {
            var seq = DOTween.Sequence();
            
            ctx.questionMark.SetActive(true);

            seq.Append(ctx.questionMark.transform.DOScale(new Vector3(1f, 1f, 1f), scaleDelay));

            ctx.questionMark.transform.localScale = new Vector3(1f, 1f, 1f);
            
            seq.Append(
                    ctx.questionMark.transform.DOShakeScale(shakeDelay, shakeScaleDelay, 
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

        private void Detectable()
        {
            detectableTimer.Value += Time.deltaTime;
        }
    }
}