using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBossBehaviour : StateMachineBehaviour
{
    BossAI ai;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.GetComponent<BossAI>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ai.currentHP > 0)
        {
            if (!ai.isStunned)
            {
                animator.SetTrigger("StunRelease");
            }
        }
        else {
            animator.SetTrigger("Death");
        }


    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("StunRelease");
    }
}
