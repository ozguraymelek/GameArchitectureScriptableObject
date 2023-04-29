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
        
        public Variable<float> delay;
        
        public float delayMin;
        public float delayMax;
        
        public Variable<bool> canCounterTimer;
        
        private PointCreator _pointCreator;
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public override void Onset(Controller.Enemy ctx)
        {
            delay.Value = Random.Range(delayMin, delayMax);
            
            if (ctx.activePoint != null)
            {
                _pointCreator = FindObjectOfType<PointCreator>();
                _pointCreator.DeactivatePoint();
            }
           
            ctx.animator.SetBool(IsWalking, false);
        }

        public override void Updating(Controller.Enemy ctx)
        {
            Counter();
        }

        private void Counter()
        {
            if (canCounterTimer.Value == false) return;
            
            idleTimer.Value += Time.deltaTime;
        }
    }
}