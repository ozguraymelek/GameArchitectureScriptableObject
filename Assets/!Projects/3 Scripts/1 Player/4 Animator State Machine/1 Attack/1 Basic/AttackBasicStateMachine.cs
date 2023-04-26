using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

public class AttackBasicStateMachine : StateMachineBehaviour
{
    public Variable<bool> isPlayerAttacking;
    public Variable<bool> isPlayerNotAttacking;
    
    private static readonly int Property = Animator.StringToHash("Basic Attack");
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isPlayerAttacking.Value = true;
        isPlayerNotAttacking.Value = false;
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isPlayerAttacking.Value = false;
        isPlayerNotAttacking.Value = true;
        
        animator.SetBool(Property, false);
    }
}
