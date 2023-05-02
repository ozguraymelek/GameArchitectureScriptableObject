using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using Nacho.Controller;
using Nacho.ObjectPools;
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
        
        // SpawnVFX(animator.GetComponent<Player>());
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isPlayerAttacking.Value = false;
        isPlayerNotAttacking.Value = true;
        
        animator.SetBool(Property, false);
    }

    //değişecek
    private void SpawnVFX(Player ctx)
    {
        var obj = FindObjectOfType<VFXPool>().Pool.Get();

        ctx.activeVFX = obj;

        ctx.activeVFX.transform.parent = ctx.weaponTr;

        ctx.activeVFX.transform.localPosition = new Vector3(0f, 0.014f, 0.309f);
        ctx.activeVFX.transform.localEulerAngles = new Vector3(-169.851f, 89.55f, -87.45001f);
    }
}
