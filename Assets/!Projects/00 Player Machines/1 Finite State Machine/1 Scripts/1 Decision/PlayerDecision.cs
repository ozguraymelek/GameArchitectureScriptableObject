using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nacho.FINITE_STATE_MACHINE
{
    public abstract class PlayerDecision : ScriptableObject
    {
        public abstract bool Decide(Controller.Player ctx);
    }
}