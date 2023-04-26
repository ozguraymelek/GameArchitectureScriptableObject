using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.FINITE_ATTACK_MACHINE
{
    public class PlayerBaseAttackState : ScriptableObject
    {
        public virtual void Onset(Controller.Player ctx) { } // working like event f Start
        public virtual void Updating(Controller.Player ctx) { } // working like event f Update
    }
}