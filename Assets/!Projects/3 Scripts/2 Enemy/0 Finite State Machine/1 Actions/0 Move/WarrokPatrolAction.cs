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
        
        private PrimitiveTypePool _waypointPool;
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public override void Onset(Controller.Enemy ctx)
        {
            _waypointPool = FindObjectOfType<PrimitiveTypePool>();
            
            ctx.animator.SetBool(IsWalking, true);
            
            input = RandomInput();
            ctx.activePoint = _waypointPool.GetPoint();
            ctx.activePoint.transform.localPosition = (input * 10f) + ctx.transform.position;
            
            _waypointPool.ActivatePoint();
        }

        public override void Updating(Controller.Enemy ctx)
        {
            if (ctx.activePoint == null) return;
            
            Move(ctx);
            Look(ctx);
        }

        private void Move(Controller.Enemy ctx)
        {
            ctx.rb.velocity = input * 2f;
        }

        private void Look(Controller.Enemy ctx)
        {
            ctx.transform.LookAt(ctx.activePoint.transform.position);
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