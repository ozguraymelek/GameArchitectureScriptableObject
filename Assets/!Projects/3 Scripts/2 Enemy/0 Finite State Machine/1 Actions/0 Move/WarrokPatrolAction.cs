using System;
using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using Nacho.ObjectPools;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Action/Patrol", fileName = "new Patrol Data")]
    public class WarrokPatrolAction : EnemyAction
    {
        public Vector3 input;
        
        private PointCreator pointCreator;

        private Vector3 _targetDirection;
        private Vector3 _enemyCalculateVector;
        
        private float angle;
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public override void Onset(Controller.Enemy ctx)
        {
            pointCreator = FindObjectOfType<PointCreator>();
            
            ctx.animator.SetBool(IsWalking, true);
            
            input = RandomInput();
            
            ctx.activePoint = pointCreator.GetPoint();
            ctx.activePoint.transform.localPosition = (input * 10f) + ctx.transform.position;
            
            pointCreator.ActivatePoint();
            
            _targetDirection = ctx.activePoint.transform.position - ctx.transform.position;
            
            _enemyCalculateVector = ctx.transform.forward;

            angle = Vector3.SignedAngle(_targetDirection,
                _enemyCalculateVector, Vector3.up);
            
            Debug.Log($"Angle: {angle}");
        }

        public override void Updating(Controller.Enemy ctx)
        {
            if (ctx.activePoint == null) return;
            
            Look(ctx);
            Move(ctx);
        }

        private void Move(Controller.Enemy ctx)
        {
            ctx.rb.velocity = _targetDirection * (15f * Time.fixedDeltaTime);
        }

        private void Look(Controller.Enemy ctx)
        {
            var targetRot = Quaternion.Euler(0, -angle
                , 0);
            
            ctx.transform.rotation = Quaternion.Lerp(ctx.transform.rotation, targetRot, 2f * Time.fixedDeltaTime);
        }
        
        private Vector3 RandomInput()
        {
            var randX = Random.Range(-1, 1.01f);
            var randZ = Random.Range(-1, 1.01f);

            var randInput = new Vector3(randX, 0f, randZ);
            
            return randInput;
        }
    }
}