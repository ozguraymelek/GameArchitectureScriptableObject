using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;

public class EnemyIdleStateMachine : StateMachineBehaviour
{
    public Variable<bool> isEnemyReached;
    
    public Variable<bool> canCounterTimer;
    
    private static readonly int IsRoaring = Animator.StringToHash("IsRoaring");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isEnemyReached.Value = false;

        RoaringChance(animator);
    }

    private void RoaringChance(Animator animator)
    {
        var rand = Random.Range(0, 100);
        
        if (rand % 2 == 0 && rand % 3 == 0)
        {
            canCounterTimer.Value = false;
            animator.SetTrigger(IsRoaring);
        }else
            canCounterTimer.Value = true;
    }
}