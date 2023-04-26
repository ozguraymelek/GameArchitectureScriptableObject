using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.FINITE_ATTACK_MACHINE
{
    public abstract class PlayerAttackAction : ScriptableObject
    {
        public abstract void Onset(Controller.Player ctx);
        public abstract void Updating(Controller.Player ctx);
    }
}