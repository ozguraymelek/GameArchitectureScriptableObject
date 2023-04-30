using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    [CreateAssetMenu(menuName = "Finite State Machine/Enemy/Decision/Detect/from Suspicion",fileName = "new Detect from Suspicion Decision Data")]
    public class WillWarrokDetectFromSuspicion : EnemyDecision
    {
        [Header("Settings /detect")]
        public Variable<float> detectableTimer;
        public float lengthOfStayTime;
        
        public override bool Decide(Controller.Enemy ctx)
        {
            var check = detectableTimer.Value >= lengthOfStayTime || ctx.detectedObjects.Length != 0;
            
            return check;
        }
    }
}