using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.FINITE_STATE_MACHINE
{
    public class PlayerBaseState : ScriptableObject
    {
        public virtual void Onset(Controller.Player ctx) { } // event f Start
        public virtual void Updating(Controller.Player ctx) { } // event f Update
    }
}