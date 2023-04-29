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

        var rand = Random.Range(0, 100);

        var lasRand = rand;
        
        Debug.Log(rand);

        if (rand % 2 == 0)
        {
            canCounterTimer.Value = false;
            animator.SetTrigger(IsRoaring);
        }else if (rand % 2 != 0 || lasRand == rand)
            canCounterTimer.Value = true;
    }
}
