using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public GameObject StunState;
    public Rigidbody2D rb;
    public Transform player;
    public Transform EnemyBody;
    public GameObject objectDeathEffect;
    public Animator enemyDeathEffect;
    public GameObject FireExplosion;
    public GameObject FireHand;
    public bool isStunned = false;
    public float stunTimer = 2f;
    public float currentStunTime;
    public bool isCatchedBySP = false;
    public float movementSpeed = 5f;
    public bool isFlippedHorizontally = false;
    public bool isFacingRight = true;
    public Vector3 distancePlayer;
    public bool isTriggered = false;

    public bool isAttacking = false;
    public bool recoverAfterAttack = false;
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
    public Vector3 startPosition;
    public int startState = 0;
    public bool StartIsFlippedHorizontally = false;
    public bool StartIsFacingRight = true;
    public Vector3 startScale;

    public string[] attacks = { "Attack1", "Attack2" };
    public string attackToDo;
    public int attack1MaxNumber = 2;
    public int attack2MaxNumber = 6;
    public int choosenAttackNumber;
    public bool attackIsChoosen = false;
    public bool endAttack;

    public int maxHP = 3;
    public int currentHP;
    public int currentPhase;
    public bool isDead=false;

    public int amountOfStormHits = 8;
    public float timeBetweenStormHit = 0.1f;
    public bool respawned=false;

    public GameObject Storm;
    public GameObject StormField;
    public Transform storm;

    public GameObject Shield;
    public Animator animatorShield;
    public float shieldTimer = 10f;
    public float currentShieldTime;
    public bool isShieldActive = true;

    public PointsController pointsController;
    [SerializeField] private int pointsToGet;
    public bool canGetPoints = true;

    public float startMovementSpeed;
    public float startTimeBetweenStormHit;
    public float startAttackRecoverTime;




    private void Start()
    {
        startPosition = this.transform.position;
        startScale = this.transform.localScale;
        currentAttackRecoverTime = attackRecoverTime;
        StunState.SetActive(false);
        currentStunTime = stunTimer;
        objectDeathEffect.SetActive(false);
        FireExplosion.SetActive(false);
        FireHand.SetActive(false);
        StormField.SetActive(false);
        currentPhase = 4 - currentHP;
        currentHP = maxHP;
        currentShieldTime = shieldTimer;
        startMovementSpeed=movementSpeed;
        startTimeBetweenStormHit=timeBetweenStormHit;
        startAttackRecoverTime=attackRecoverTime;

}

    // Update is called once per frame
    void Update()
    {
        FlipHorizontally();
        if (!isDead)
        {
            if (isTriggered)
            {
                ShieldController();
                ChooseAttack();
                StunStating();
            }
        }

    }
    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (isTriggered)
            {
                CheckPlayerDistance();
                AttackPlayer();
            }
        }
    }
    public void FlipHorizontally()
    {
        if (rb.velocity.x > 0f && isFlippedHorizontally)
        {
            isFacingRight = true;
            isFlippedHorizontally = false;
            Vector3 flip = EnemyBody.localScale;
            flip.x *= -1;
            EnemyBody.localScale = flip;
        }
        else if (rb.velocity.x < 0f && !isFlippedHorizontally)
        {
            isFacingRight = false;
            isFlippedHorizontally = true;
            Vector3 flip = EnemyBody.localScale;
            flip.x *= -1;
            EnemyBody.localScale = flip;

        }

    }
    public void CheckPlayerDistance()
    {
        distancePlayer = player.position - transform.position;
    }
    private void StunStating()
    {
        if (isStunned)
        {
            rb.velocity = new Vector2(0, 0);
            if (!isCatchedBySP)
            {
                if (currentStunTime > 0)
                {
                    currentStunTime -= Time.deltaTime;
                }
                else
                {
                    currentStunTime = stunTimer;
                    isStunned = !isStunned;

                }
                StunState.SetActive(isStunned);
            }

        }
        else
        {
            StunState.SetActive(isStunned);
        }
    }
    public void ChooseAttack() {
        if (!attackIsChoosen && !recoverAfterAttack) {
            choosenAttackNumber = Random.Range(1, attack2MaxNumber + 1);
            if ((choosenAttackNumber == 1 || choosenAttackNumber == 5) && choosenAttackNumber >= 0)
            {
                attackToDo = attacks[0];
            }
            else {
                attackToDo = attacks[1];
            }
            attackIsChoosen = true;
        }
    }
    public void GetDamage() {
        currentHP -= 1;
    }
    public void AttackPlayer()
    {
        switch (attackToDo) {
            case "Attack1":
                reactionRange = attackReaction.position.x - transform.position.x;
                if ((distancePlayer.x < 0 && distancePlayer.x > reactionRange || distancePlayer.x > 0 && distancePlayer.x < reactionRange) && !isAttacking && !recoverAfterAttack && !isStunned)
                {
                    if (!isStunned)
                    {
                        isAttacking = true;
                        rb.velocity = new Vector2(0, 0);
                    }
                }
                break;
            case "Attack2":
                if (!isStunned)
                {
                    isAttacking = true;
                    if (distancePlayer.x < 0)
                    {
                        rb.velocity = new Vector2(-0.01f, 0);
                    }
                    else if (distancePlayer.x > 0)
                    {
                        rb.velocity = new Vector2(0.01f, 0);
                    }

                }
                break;
            default:
                isAttacking = false;
                break;
        }
        if (!isAttacking && recoverAfterAttack)
        {
            if (currentAttackRecoverTime <= 0)
            {
                currentAttackRecoverTime = attackRecoverTime;
                recoverAfterAttack = !recoverAfterAttack;
            }
            else
            {
                currentAttackRecoverTime -= Time.deltaTime;
            }
        }
    }
    public void AttackHit()
    {
        if (attackPoint != null)
        {
            Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, Player);
            if (hitPlayer != null)
            {
                hitPlayer.gameObject.GetComponent<PlayerController>().Death();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void EndAttack() {
        endAttack = true;
    }
    public void FireExplode() {
        FireExplosion.SetActive(true);
    }
    public void FireHandSummon()
    {
        if (distancePlayer.x < 0)
        {
            rb.velocity = new Vector2(-0.01f, 0);
        }
        else if (distancePlayer.x > 0)
        {
            rb.velocity = new Vector2(0.01f, 0);
        }
        FireHand.SetActive(true);
    }
    IEnumerator StormShooting() {
        respawned = false;
        StormField.SetActive(true);
        for (int i = 0; i <= amountOfStormHits; i++) {
            if (!respawned)
            {
                Instantiate(Storm, new Vector3(player.position.x, storm.position.y, 0), Quaternion.identity);
                yield return new WaitForSeconds(timeBetweenStormHit);
            }
            if (i == amountOfStormHits) {
                StormField.SetActive(false);
                EndAttack();
            }
        }

    }
    public void stormSummon(){
        StartCoroutine("StormShooting"); 
    }
    public void SetStun()
    {
        animator.SetTrigger("Stun");
        isAttacking = false;
        recoverAfterAttack = false;
        currentAttackRecoverTime = attackRecoverTime;
        isStunned = true;
    }
    public void GiveDamageToBoss() {
        currentHP -= 1;
        objectDeathEffect.SetActive(true);
        if (currentHP == 0)
        {
            Death();
        }

        PhaseController();
    }
    public void Death() {
        isDead = true;
        isStunned = false;
        if (canGetPoints)
        {
            pointsController.AddPoints(pointsToGet);
            canGetPoints = false;
        }
        StunState.SetActive(false);
    }
    public void PhaseController() {
        currentPhase = 4 - currentHP;
        if (currentPhase > 1)
        {
            movementSpeed *= (1f + (0.2f * (currentPhase - 1)));
            timeBetweenStormHit -= (0.03f * (currentPhase - 1));
            attackRecoverTime -= (0.25f * (currentPhase - 1));
            currentAttackRecoverTime = attackRecoverTime;
        }
        else {
            movementSpeed = startMovementSpeed;
            timeBetweenStormHit = startTimeBetweenStormHit;
            attackRecoverTime = startAttackRecoverTime;
        }
    }
    public void ShieldController() {
        if (isShieldActive)
            if (currentShieldTime >= 0)
            {
                currentShieldTime -= Time.deltaTime;
            }
            else {
                animatorShield.SetTrigger("Explode");
                isShieldActive = false;
                currentShieldTime =shieldTimer;

            }
    }



}
