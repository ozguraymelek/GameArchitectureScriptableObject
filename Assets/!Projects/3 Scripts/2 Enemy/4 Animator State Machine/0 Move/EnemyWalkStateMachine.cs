using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

public class EnemyWalkStateMachine : StateMachineBehaviour
{
    public Variable<float> idleTimer;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer.Value = 0;
    }
}
