using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

public class EnemyIdleStateMachine : StateMachineBehaviour
{
    public Variable<bool> isEnemyReached; 
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isEnemyReached.Value = false;
    }
}
