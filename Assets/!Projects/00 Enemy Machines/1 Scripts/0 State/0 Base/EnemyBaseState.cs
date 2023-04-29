using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    public class EnemyBaseState : ScriptableObject
    {
        public virtual void Onset(Controller.Enemy ctx) { } // event f Start
        public virtual void Updating(Controller.Enemy ctx) { } // event f Update
        
        public virtual void OnDrawingGizmosSelected(Controller.Enemy ctx) { } // event f OnDrawGizmos
    }
}