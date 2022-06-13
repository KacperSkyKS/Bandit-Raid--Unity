using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private float trapRadius;
    [SerializeField] private float trapBoxRadiusX;
    [SerializeField] private float trapBoxRadiusY;
    [SerializeField] Transform trapPosition;
    [SerializeField] private float trapCooldown;
    private float currentTrapCooldown;
    private bool onCooldown;
    private bool canBeHit;
    public LayerMask Player;
    AnimatorStateInfo currentState;
    private int stateName;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        currentState = animator.GetCurrentAnimatorStateInfo(0);
        stateName = currentState.nameHash;
        currentTrapCooldown = trapCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        TrapCooldown();
    }
    private void FixedUpdate()
    {
        if (!onCooldown && canBeHit)
        {
            if (this.transform.tag == "Trap1")
            {
                Collider2D hitPlayer = Physics2D.OverlapBox(trapPosition.position, new Vector2(trapBoxRadiusX, trapBoxRadiusY),0,Player);
                if (hitPlayer != null)
                {
                    hitPlayer.gameObject.GetComponent<PlayerController>().Death();
                }
            }
            else if(this.transform.tag == "Trap2")
            {
                Collider2D hitPlayer = Physics2D.OverlapCircle(trapPosition.position, trapRadius, Player);
                if (hitPlayer != null)
                {
                    hitPlayer.gameObject.GetComponent<PlayerController>().Death();
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (this.transform.tag == "Trap1")
        {
            Gizmos.DrawWireCube(trapPosition.position, new Vector3(trapBoxRadiusX, trapBoxRadiusY));
        }
        else {
            Gizmos.DrawWireSphere(trapPosition.position, trapRadius);
        } 
    }
    void TrapCooldown() {
        if (onCooldown) {
            if (currentTrapCooldown <= 0f)
            {
                onCooldown = false;
                animator.enabled = true;
                currentTrapCooldown = trapCooldown;
            }
            else {
                currentTrapCooldown -= Time.deltaTime;
            }
        }

    }
    public void SetCooldown()
    {
        animator.enabled = false;
        onCooldown = true;
    }
    public void SetCanBeHit() {
        canBeHit = !canBeHit;
    }
}
