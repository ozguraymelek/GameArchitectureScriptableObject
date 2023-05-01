using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Decision/Patrol/from Detect",fileName = "new Patrol from Detect Decision Data")]
    public class WillWarrokPatrolFromDetect : EnemyDecision
    {
        public Variable<float> undetectableTimer;
        
        public override bool Decide(Controller.Enemy ctx)
        {
            var check = false;

            if (undetectableTimer.Value >= 3)
            {
                if (ctx.detectedObjects.Length == 0)
                {
                    check = true;
                }
                else if (ctx.detectedObjects.Length != 0)
                {
                    check = false;
                }
            }

            return check;
        }
    }
}