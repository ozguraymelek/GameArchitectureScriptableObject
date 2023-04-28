using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.Enemy.FINITE_STATE_MACHINE
{
    public abstract class EnemyDecision : ScriptableObject
    {
        public abstract bool Decide(Controller.Enemy ctx);
    }
}
