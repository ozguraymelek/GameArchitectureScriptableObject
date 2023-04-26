using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.FINITE_STATE_MACHINE
{
    public abstract class PlayerAction : ScriptableObject
    {
        public abstract void Onset(Controller.Player ctx);
        public abstract void Updating(Controller.Player ctx);
    }
}