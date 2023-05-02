using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using Nacho.Controller;
using Nacho.ObjectPools;
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

        // DespawnVFX(animator.GetComponent<Player>());
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(Property, basicAttackValue.Value);
    }
    
    //değişecek
    private void DespawnVFX(Player ctx)
    {
        if (ctx.activeVFX == null) return;
        
        ctx.activeVFX.transform.parent = null;
        
        ctx.activeVFX.ReleaseVfx();
    }
}
