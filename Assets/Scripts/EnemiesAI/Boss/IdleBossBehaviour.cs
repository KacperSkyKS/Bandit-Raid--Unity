using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBossBehaviour : StateMachineBehaviour
{
    BossAI ai;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.GetComponent<BossAI>();

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ai.attackToDo != null && !ai.recoverAfterAttack) {
            animator.SetTrigger("WantAttack");
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("WantAttack");
    }

}
