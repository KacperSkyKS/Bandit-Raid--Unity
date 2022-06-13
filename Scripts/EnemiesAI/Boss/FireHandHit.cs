using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHandHit : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.2f;
    public LayerMask Player;
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
    public void FireDisappear() {
        this.gameObject.SetActive(false);
    }
}
