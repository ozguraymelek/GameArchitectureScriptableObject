using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.FINITE_ATTACK_MACHINE
{
    public abstract class PlayerAttackDecision : ScriptableObject
    {
        public abstract bool Decide(Controller.Player ctx);
    }
}