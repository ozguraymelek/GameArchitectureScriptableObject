using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UniRx.Triggers;
using UnityEngine;

public class AttackNullStateMachine : StateMachineBehaviour
{
    public Variable<int> basicAttackValue;
    
    public Variable<bool> isPlayerAttacking;
    
    private static readonly int Property = Animator.StringToHash("Basic Attack Value");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isPlayerAttacking.Value = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(Property, basicAttackValue.Value);
    }
}
