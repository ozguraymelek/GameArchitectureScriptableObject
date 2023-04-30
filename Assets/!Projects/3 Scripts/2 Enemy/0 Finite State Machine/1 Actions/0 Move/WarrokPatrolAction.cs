using System;
using System.Collections;
using System.Collections.Generic;
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
        private PointCreator pointCreator;
        
        [Header("Settings /transition")]
        public Variable<float> delay;
        
        [Header("Settings /patrol")]
        public Vector3 input;
        private Vector3 _targetDirection;
        private Vector3 _enemyCalculateVector;
        private float _currentEnemyEulerY;
        private float _angle;
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public override void Onset(Controller.Enemy ctx)
        {
            pointCreator = FindObjectOfType<PointCreator>();

            delay.Value = 0;
            
            ctx.animator.SetBool(IsWalking, true);
            
            input = RandomInput();

            GetPoint(ctx);

            CalculateAngle(ctx);
        }

        public override void Updating(Controller.Enemy ctx)
        {
            base.Raycast(ctx);
            
            if (ctx.activePoint == null) return;
            
            Look(ctx);
            Move(ctx);
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
            ctx.activePoint = pointCreator.GetPoint();
            ctx.activePoint.transform.localPosition = (input * 10f) + ctx.transform.position;
            
            pointCreator.ActivatePoint();
        }
        
        private void CalculateAngle(Controller.Enemy ctx)
        {
            _targetDirection = ctx.activePoint.transform.position - ctx.transform.position;
            
            _enemyCalculateVector = ctx.transform.forward;
            
            _currentEnemyEulerY = ctx.transform.localEulerAngles.y - 360f;

            _angle = Vector3.SignedAngle(_targetDirection,
                _enemyCalculateVector, Vector3.up);
            
            Debug.Log($"Angle: {_angle}");
        }
        
        private Vector3 RandomInput()
        {
            var randX = Random.Range(-1, 1.01f);
            var randZ = Random.Range(-1, 1.01f);

            var randInput = new Vector3(randX, 0f, randZ);
            
            return randInput;
        }
        
        public override void OnDrawingGizmosSelected(Controller.Enemy ctx)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawSphere(ctx.transform.position + new Vector3(0f, ctx.transform.localScale.y, 0f),
                suspicionRadius.Value);
        }
        
    }
}