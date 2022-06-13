using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform body;
    //[SerializeField] ParticleSystem fireParticle;
    [SerializeField] GameObject fireAttack;
    public GameObject choosenEnemy;
    public EnemyAI choosenEnemyAI;
    public BossAI choosenBossAI;
    public Animator playerAnimator;
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    public float pickingForceX=15f;
    public float pickingForceY=-10f;
    public float bounceForceX = -5f;
    public float bounceForceY = 10f;
    private bool _onGround;
    public bool onGround
    {
        get { return _onGround; }
        set
        {
            if (_onGround == false && value == true)
            {
                playerAnimator.SetTrigger("StopJumping");
            }
            _onGround = value;
        }
    }
    public bool isPicking;
    public bool isBounced;
    public bool isFlippedHorizontally = false;
    public bool isFacingRight = true;
    public bool makeSpecialAttack;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckRadius;

    public bool isDead = false;
    [SerializeField] private GameObject deathScreen;

    // Start is called before the first frame update
    private void Start()
    {
        fireAttack.SetActive(false);
        onGround = false;
        deathScreen.SetActive(false);
    }
    public void Move()
    {
        if (!isPicking && !isBounced)
        {
            float x = Input.GetAxis("Horizontal") * movementSpeed;
            float y = rb.velocity.y;
            rb.velocity = new Vector2(x, y);
        }
    }
    public void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            Vector2 jump = new Vector2(0f, jumpForce);
            rb.velocity = jump;
        }
    }
    public void Picking() {
        if (Input.GetKeyDown(KeyCode.Space) && !onGround && !isBounced && rb.velocity.y > 0) {
            isPicking = true;
            float realPickingForceX = pickingForceX;
            if (!isFacingRight) {
                realPickingForceX *= -1f;  
            }
            rb.velocity += new Vector2(realPickingForceX, pickingForceY);
        }
    }
    public void FlipHorizontally()
    {
        if (!isBounced)
        {
            if (rb.velocity.x > 0 && isFlippedHorizontally)
            {
                isFacingRight = true;
                isFlippedHorizontally = false;
                Vector3 flip = body.localScale;
                flip.x *= -1;
                body.localScale = flip;
            }
            else if (rb.velocity.x < 0 && !isFlippedHorizontally)
            {
                isFacingRight = false;
                isFlippedHorizontally = true;
                Vector3 flip = body.localScale;
                flip.x *= -1;
                body.localScale = flip;

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            Jump();
            Picking();
            SpecialAttack();
            FlipHorizontally();
            PlayAnimation();
        }
    }
    private void FixedUpdate()
    {
        if (!isDead)
        {
            CheckGround();
        }
    }
    /*void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag=="Ground")
        {
            onGround = true;
            isPicking = false;
            isBounced = false;
            makeSpecialAttack = false;
            fireAttack.SetActive(false);
        }
    }*/
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Enemy" && isPicking && other.gameObject.GetComponent<EnemyAI>().currentState!=4)
        {
            isPicking = false;
            isBounced = true;
            ChooseTarget(other.gameObject);
            other.gameObject.GetComponent<EnemyAI>().SetStun();
            float realBounceForceX = bounceForceX;
            if (!isFacingRight)
            {
                realBounceForceX *= -1f;
            }
            rb.velocity = new Vector2(realBounceForceX, bounceForceY);
        }
        if (other.transform.tag == "Boss" && isPicking) {
            if (!other.gameObject.GetComponent<BossAI>().isShieldActive)
            {
                isPicking = false;
                isBounced = true;
                ChooseTarget(other.gameObject);
                other.gameObject.GetComponent<BossAI>().SetStun();
                float realBounceForceX = bounceForceX;
                if (!isFacingRight)
                {
                    realBounceForceX *= -1f;
                }
                Debug.Log(realBounceForceX);
                rb.velocity = new Vector2(realBounceForceX, bounceForceY);
            }else if(other.gameObject.GetComponent<BossAI>().isShieldActive)
            {
                isPicking = false;
                isBounced = true;
                float realBounceForceX = bounceForceX;
                if (!isFacingRight)
                {
                    realBounceForceX *= -1f;
                }
                rb.velocity = new Vector2(realBounceForceX, bounceForceY); ;
            }
        }
        if (other.gameObject == choosenEnemy && makeSpecialAttack)
        {
            if (choosenEnemy.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyAI>().Death();
            }
            if (choosenEnemy.tag == "Boss")
            {
                other.gameObject.GetComponent<BossAI>().GiveDamageToBoss();
                other.gameObject.GetComponent<BossAI>().isCatchedBySP = false;
                other.gameObject.GetComponent<BossAI>().currentStunTime = other.gameObject.GetComponent<BossAI>().stunTimer;
                if (!other.gameObject.GetComponent<BossAI>().isDead)
                {
                    other.gameObject.GetComponent<BossAI>().Shield.SetActive(true);
                    other.gameObject.GetComponent<BossAI>().isShieldActive = true;
                }
            }
        }

    }
    void ChooseTarget(GameObject target) {
        choosenEnemy = target;
        if (target.tag == "Enemy")
        {
            choosenEnemyAI = target.GetComponent<EnemyAI>();
        }
        if (target.tag == "Boss")
        {
            choosenBossAI = target.GetComponent<BossAI>();
        }
    }
    void SpecialAttack(){
        if (choosenEnemy != null)
        {
                switch (choosenEnemy.tag) {
                    case "Enemy":
                        if (!choosenEnemyAI.isStunned)
                        {
                            choosenEnemy = null;
                            choosenEnemyAI = null;
                            makeSpecialAttack = false;
                        }
                        else {
                            if (Input.GetKeyDown(KeyCode.Z) && isBounced)
                            {
                                makeSpecialAttack = true;
                                choosenEnemyAI.isCatchedBySP = true;
                                Vector2 direction = choosenEnemy.transform.position - transform.position;
                                if (isFacingRight)
                                {
                                    rb.velocity = new Vector2(direction.x + 10, direction.y - 3);
                                }
                                else
                                {
                                    rb.velocity = new Vector2(direction.x - 10, direction.y - 3);
                                }
                                //fireParticle.Play();
                                fireAttack.SetActive(true);
                            }
                    }
                        break;
                    case "Boss":
                        if (!choosenBossAI.isStunned)
                        {
                            choosenEnemy = null;
                            choosenBossAI = null;
                            makeSpecialAttack = false;
                        }
                        else {
                            if (Input.GetKeyDown(KeyCode.Z) && isBounced)
                            {
                                makeSpecialAttack = true;
                                choosenBossAI.isCatchedBySP = true;
                                Vector2 direction = choosenEnemy.transform.position - transform.position;
                                if (isFacingRight)
                                {
                                    rb.velocity = new Vector2(direction.x + 10, direction.y - 3);
                                }
                                else
                                {
                                    rb.velocity = new Vector2(direction.x - 10, direction.y - 3);
                                }
                                //fireParticle.Play();
                                fireAttack.SetActive(true);
                            }
                    }
                        break;
                    default:
                        break;
                }
            
        }
    }
    void PlayAnimation() {
        if (rb.velocity.x != 0 && onGround)
        {
            playerAnimator.SetBool("isMoving", true);
        }
        else {
            playerAnimator.SetBool("isMoving", false);
        }
        if (!onGround)
        {
            playerAnimator.SetBool("isJumping", true);
        }
        else{
            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("isFalling", false);
            playerAnimator.SetBool("isPicking", false);
            playerAnimator.SetBool("isBounced", false);
            playerAnimator.SetBool("makeSpecialAttack", false);
        }
        if (rb.velocity.y < 0 && !onGround) {
            playerAnimator.SetBool("isFalling", true);
            playerAnimator.SetBool("isJumping", false);
        }
        if (isPicking) {
            playerAnimator.SetBool("isPicking", true);
            playerAnimator.SetBool("isFalling", false);
        }
        if (isBounced) {
            playerAnimator.SetBool("isPicking", false);
            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("isBounced", true);
        }
        if (makeSpecialAttack)
        {
            playerAnimator.SetBool("makeSpecialAttack", true);
        }
        else {
            playerAnimator.SetBool("makeSpecialAttack", false);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Ground")
        {
            onGround = false;
        }
    }
    private void CheckGround()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (onGround) {
            isPicking = false;
            isBounced = false;
            makeSpecialAttack = false;
            fireAttack.SetActive(false);
        }
    }
    public void Death() {
        if (!isDead)
        {
            isDead = true;
            deathScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
