using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    public abstract class EnemyAction : ScriptableObject
    {
        public abstract void Onset(Controller.Enemy ctx);
        public abstract void Updating(Controller.Enemy ctx);
    }
}