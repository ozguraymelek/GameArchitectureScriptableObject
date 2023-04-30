using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using JetBrains.Annotations;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    public abstract class EnemyAction : ScriptableObject
    {
        [Header("Settings /suspicion")]
        [CanBeNull] public Variable<float> suspicionRadius;
        public LayerMask suspicionLayer;
        
        [Header("Settings /detect")]
        [CanBeNull] public Variable<float> detectRadius;
        public LayerMask detectLayer;
        
        public abstract void Onset(Controller.Enemy ctx);
        public abstract void Updating(Controller.Enemy ctx);
        public virtual void OnDrawingGizmosSelected(Controller.Enemy ctx) { }

        protected virtual void Raycast(Controller.Enemy ctx)
        {
            ctx.suspicionObjects = Physics.OverlapSphere(ctx.transform.position, suspicionRadius.Value,
                suspicionLayer);
        }
    }
}