using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBossBehaviour : StateMachineBehaviour
{
    BossAI ai;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.GetComponent<BossAI>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ai.endAttack) {
            ai.isAttacking = false;
            ai.recoverAfterAttack = true;
            animator.SetTrigger(ai.attackToDo);
        }

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(ai.attackToDo);
        ai.attackToDo = null;
        ai.attackIsChoosen = false;
        ai.endAttack = false;
    }
}
