using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using Nacho.Controller;
using Nacho.ObjectPools;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Action/Idle", fileName = "new Idle Data")]
    public class WarrokIdleAction : EnemyAction
    {
        public Variable<float> idleTimer; 
        
        private PrimitiveTypePool _waypointPool;
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public override void Onset(Controller.Enemy ctx)
        {
            if (ctx.activePoint != null)
            {
                _waypointPool = FindObjectOfType<PrimitiveTypePool>();
                _waypointPool.DeactivatePoint();
            }
           
            ctx.animator.SetBool(IsWalking, false);
        }

        public override void Updating(Controller.Enemy ctx)
        {
            Counter();
        }

        private void Counter()
        {
            idleTimer.Value += Time.deltaTime;
        }
    }
}