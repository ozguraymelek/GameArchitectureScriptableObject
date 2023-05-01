using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Action/Attack", fileName = "new Attack Data")]
    public class WarrokAttackAction : EnemyAction
    {
        [Header("Settings /detect")]
        public Variable<float> attackRadius;
        public LayerMask attackLayer;
        
        [Header("Settings /suspicion")]
        public Variable<float> suspicionRadius;
        public LayerMask suspicionLayer;
        
        [Header("Settings /animation keywords")]
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

        public override void Onset(Controller.Enemy ctx)
        {
            ChangeTargetLayer(ctx);
            
            ctx.animator.SetBool(IsAttacking, true);

        }

        public override void Updating(Controller.Enemy ctx)
        {
            AttackRaycast(ctx);
            SuspicionRaycast(ctx);
        }

        protected override void AttackRaycast(Controller.Enemy ctx)
        {
            ctx.attackableObjects = Physics.OverlapSphere(
                ctx.transform.position + new Vector3(0f, ctx.transform.localScale.y, 0f),
                attackRadius.Value, attackLayer);
        }
        
        protected override void SuspicionRaycast(Controller.Enemy ctx)
        {
            ctx.suspicionObjects = Physics
                .OverlapSphere(ctx.transform.position
                               + new Vector3(0f, ctx.transform.localScale.y, 0f),
                    suspicionRadius.Value,suspicionLayer);
        }
        
        private void ChangeTargetLayer(Controller.Enemy ctx)
        {
            ctx.attackableObjects[0].gameObject.layer = LayerMask.NameToLayer("Attackable");
        }
        
        public override void OnDrawingGizmosSelected(Controller.Enemy ctx)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(ctx.transform.position + new Vector3(0f, ctx.transform.localScale.y, 0f),
                attackRadius.Value);
        }
    }
}

