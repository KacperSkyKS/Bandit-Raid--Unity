using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] public GameObject StunState;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Transform player;
    [SerializeField] public Transform EnemyBody;
    [SerializeField] public GameObject objectDeathEffect;
    [SerializeField] public Animator enemyDeathEffect;
    public bool isStunned = false;
    public float stunTimer = 2f;
    public float currentStunTime;
    public bool isCatchedBySP = false;
    [SerializeField] private float movementSpeed = 5f;
    public bool isFlippedHorizontally = false;
    public bool isFacingRight = true;
    public Vector3 distancePlayer;
    public bool isTriggered = false;

    public bool isAttacking = false;
    public bool recoverAfterAttack=false;
    public Transform attackPoint;
    public Animator animator;
    public float attackRange = 0.2f;
    public Transform attackReaction;
    public float reactionRange;
    public LayerMask Player;
    public float attackRecoverTime = 1.5f;
    public float currentAttackRecoverTime;
    public int currentState;
    /*
     * 0-Idle
     * 1-Move
     * 2-Attack
     * 3-Stun
     * 4-Death
     */
    [SerializeField] public PointsController pointsController;
    [SerializeField] private int pointsToGet;
    public bool canGetPoints = true;

    public Vector3 startPosition;
    public int startState = 0;
    public bool StartIsFlippedHorizontally = false;
    public bool StartIsFacingRight = true;
    public Vector3 startScale;
    private void Start()
    {
        pointsController= GameObject.FindWithTag("Points").GetComponent<PointsController>();
        startPosition = this.transform.position;
        startScale = this.transform.localScale;
        currentAttackRecoverTime = attackRecoverTime;
        StunState.SetActive(false);
        currentStunTime = stunTimer;
        objectDeathEffect.SetActive(false);
    }
    void Update()
    {
        AnimationController();
        FlipHorizontally();
        if (isTriggered)
        {
            StunStating();
        }
    }
    private void FixedUpdate()
    {
        if (currentState != 4)
        {
            if (isTriggered)
            {
                CheckPlayerDistance();
                if (currentState == 1)
                {
                    Move();
                }
                AttackPlayer();
            }
        }
    }

    private void StunStating() {
        if (isStunned)
        {
            currentState = 3;
            rb.velocity = new Vector2(0, 0);
            if (!isCatchedBySP) {
                if (currentStunTime > 0)
                {
                    currentStunTime -= Time.deltaTime;
                }
                else {
                    animator.SetTrigger("StunRelease");
                    currentStunTime = stunTimer;

                }
                StunState.SetActive(isStunned);    
            }
            
        }
        else {
            StunState.SetActive(isStunned);
        }
    }
    public void Move()
    {
        animator.SetBool("isMoving",true);
        float x=0f;
        if (distancePlayer.x < 0)
        {
            x = movementSpeed * -1;
        }
        else if (distancePlayer.x > 0)
        {
            x = movementSpeed;
        }
        float y = rb.velocity.y;
        rb.velocity = new Vector2(x, y);
    }
    public void FlipHorizontally()
    {
            if (rb.velocity.x > 0 && isFlippedHorizontally)
            {
                isFacingRight = true;
                isFlippedHorizontally = false;
                Vector3 flip = EnemyBody.localScale;
                flip.x *= -1;
                EnemyBody.localScale = flip;
            }
            else if (rb.velocity.x < 0 && !isFlippedHorizontally)
            {
                isFacingRight = false;
                isFlippedHorizontally = true;
                Vector3 flip = EnemyBody.localScale;
                flip.x *= -1;
                EnemyBody.localScale = flip;

            }

    }
    public void CheckPlayerDistance() {
        distancePlayer = player.position - transform.position;
    }
    public void AttackPlayer() {
        reactionRange = attackReaction.position.x - transform.position.x;
        if ((distancePlayer.x < 0 && distancePlayer.x > reactionRange || distancePlayer.x > 0 && distancePlayer.x < reactionRange)&& !isAttacking && !recoverAfterAttack && !isStunned)
        {
            currentState = 2;
            isAttacking = true;
            rb.velocity = new Vector2(0, 0);
            if (!isStunned) {
                animator.SetTrigger("Attack");
            }
        }
        if(!isAttacking && recoverAfterAttack){
            if (currentAttackRecoverTime <= 0)
            {
                currentState = 1;
                currentAttackRecoverTime = attackRecoverTime;
                recoverAfterAttack = !recoverAfterAttack;
            }
            else {
                currentAttackRecoverTime -= Time.deltaTime;
            }
        }
    }
    public void AnimationController() {
        if (rb.velocity.x == 0) {
            animator.SetBool("isMoving", false);
        }
    }
    public void SetIsAttacking() {
        isAttacking = false;
        recoverAfterAttack = true;
    }
    public void SetStun() {
        animator.SetTrigger("Stun");
        isAttacking = false;
        recoverAfterAttack = false;
        currentAttackRecoverTime = attackRecoverTime;
        isStunned = true;
    }
    public void StunRelease() {
        isAttacking = false;
        isStunned = false;
        animator.SetTrigger("StunRelease");
        currentStunTime = stunTimer;
        currentState = 1;
    }
    public void AttackHit() {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, Player);
        if (hitPlayer != null)
        {
            hitPlayer.gameObject.GetComponent<PlayerController>().Death();
        }
    }
    public void Death() {
        isStunned = false;
        currentState = 4;
        objectDeathEffect.SetActive(true);
        transform.parent.gameObject.GetComponent<FightController>().currentAmountOfEnemies--;
        StunState.SetActive(false);
        if (canGetPoints)
        {
            pointsController.AddPoints(pointsToGet);
            canGetPoints = false;
        }
        animator.SetTrigger("Death");
    }
    private void OnDrawGizmosSelected()
    {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void BackToLife() {
        animator.SetTrigger("BackToLife");
    }

}
