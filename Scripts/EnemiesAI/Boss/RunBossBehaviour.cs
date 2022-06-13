using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBossBehaviour : StateMachineBehaviour
{
    public Transform player;
    public Rigidbody2D rbBoss;
    public BossAI ai;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rbBoss = animator.GetComponent<Rigidbody2D>();
        ai= animator.GetComponent<BossAI>();

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!ai.isStunned)
        {
            if (ai.isTriggered)
            {
                if (ai.attackToDo == ai.attacks[0])
                {
                    if (ai.isAttacking)
                    {
                        animator.SetTrigger(ai.attackToDo);
                    }
                    else
                    {
                        float x = 0f;
                        if (ai.distancePlayer.x < 0)
                        {
                            x = ai.movementSpeed * -1;
                        }
                        else if (ai.distancePlayer.x > 0)
                        {
                            x = ai.movementSpeed;
                        }
                        float y = rbBoss.velocity.y;
                        rbBoss.velocity = new Vector2(x, y);
                    }
                }
                else if (ai.attackToDo == ai.attacks[1])
                {
                    animator.SetTrigger(ai.attackToDo);
                }
            }
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(ai.attackToDo);
    }
}
