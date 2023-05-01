using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using JetBrains.Annotations;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    public abstract class EnemyAction : ScriptableObject
    {
        public abstract void Onset(Controller.Enemy ctx);
        public abstract void Updating(Controller.Enemy ctx);
        public virtual void OnDrawingGizmosSelected(Controller.Enemy ctx) { }

        protected virtual void SuspicionRaycast(Controller.Enemy ctx)
        {
            
        }
        
        protected virtual void DetectRaycast(Controller.Enemy ctx)
        {
            
        }
        
        protected virtual void AttackRaycast(Controller.Enemy ctx)
        {
            
        }
    }
}