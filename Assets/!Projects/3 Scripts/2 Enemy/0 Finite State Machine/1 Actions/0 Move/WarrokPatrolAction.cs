using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GenericScriptableArchitecture;
using Nacho.ObjectPools;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Action/Patrol", fileName = "new Patrol Data")]
    public class WarrokPatrolAction : EnemyAction
    {
        [Header("Ref")]
        private PointCreator _pointCreator;
        
        [Header("Settings /transition")]
        public Variable<float> delay;
        
        [Header("Settings /detect")]
        public Variable<float> undetectableTimer;
        
        [Header("Settings /suspicion")]
        public Variable<float> suspicionRadius;
        public LayerMask suspicionLayer;
        
        [Header("Settings /patrol")]
        public Vector3 input;
        private Vector3 _targetDirection;
        private Vector3 _enemyCalculateVector;
        private float _currentEnemyEulerY;
        private float _angle;
        
        [Header("Settings /question mark")]
        public float scaleDelay;
        
        [Header("Settings /animation keywords")]
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public override void Onset(Controller.Enemy ctx)
        {
            _pointCreator = FindObjectOfType<PointCreator>();
            
            if (ctx.questionMark.activeSelf == true)
                UnscalingQuestionMark(ctx);
            
            delay.Value = 0;
            
            ctx.animator.SetBool(IsWalking, true);
            
            input = RandomInput();

            GetPoint(ctx);

            CalculateAngle(ctx);

            ChangeTargetLayer(ctx);
        }

        public override void Updating(Controller.Enemy ctx)
        {
            SuspicionRaycast(ctx);
            
            if (ctx.activePoint == null) return;
            
            Look(ctx);
            Move(ctx);
        }

        protected override void SuspicionRaycast(Controller.Enemy ctx)
        {
            ctx.suspicionObjects = Physics
                .OverlapSphere(ctx.transform.position
                                       + new Vector3(0f, ctx.transform.localScale.y, 0f),
                    suspicionRadius.Value,suspicionLayer);
        }

        private void Move(Controller.Enemy ctx)
        {
            ctx.rb.velocity = _targetDirection * (10f * Time.fixedDeltaTime);
        }

        private void Look(Controller.Enemy ctx)
        {
            var targetRot = Quaternion.Euler(0f, _currentEnemyEulerY - _angle
                , 0f);
            
            ctx.transform.rotation = Quaternion.Slerp(ctx.transform.rotation, targetRot, 1.2f * Time.fixedDeltaTime);
        }

        private void GetPoint(Controller.Enemy ctx)
        {
            ctx.activePoint = _pointCreator.GetPoint();
            ctx.activePoint.transform.localPosition = (input * 10f) + ctx.transform.position;
            
            _pointCreator.ActivatePoint();
        }
        
        private void CalculateAngle(Controller.Enemy ctx)
        {
            _targetDirection = ctx.activePoint.transform.position - ctx.transform.position;
            
            _enemyCalculateVector = ctx.transform.forward;
            
            _currentEnemyEulerY = ctx.transform.localEulerAngles.y - 360f;

            _angle = Vector3.SignedAngle(_targetDirection,
                _enemyCalculateVector, Vector3.up);
        }
        
        private Vector3 RandomInput()
        {
            var randX = Random.Range(-1, 1.01f);
            var randZ = Random.Range(-1, 1.01f);

            var randInput = new Vector3(randX, 0f, randZ);
            
            return randInput;
        }
        
        private void UnscalingQuestionMark(Controller.Enemy ctx)
        {
            var seq = DOTween.Sequence();
            
            seq.Append(ctx.questionMark.transform.DOScale(new Vector3(0f, 0f, 0f), scaleDelay));
            
            seq.AppendCallback(() =>
            {
                ctx.questionMark.SetActive(false);
            });
        }
        
        private void ChangeTargetLayer(Controller.Enemy ctx)
        {
            if (ctx.activePlayer == null) return;
            
            ctx.activePlayer.gameObject.layer = LayerMask.NameToLayer("Player");
        }
        
        public override void OnDrawingGizmosSelected(Controller.Enemy ctx)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawSphere(ctx.transform.position + new Vector3(0f, ctx.transform.localScale.y, 0f),
                suspicionRadius.Value);
        }
    }
}