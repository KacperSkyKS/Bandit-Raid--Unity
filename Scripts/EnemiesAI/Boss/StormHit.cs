using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormHit : MonoBehaviour
{
    [SerializeField] private float stormBoxRadiusX;
    [SerializeField] private float stormBoxRadiusY;
    [SerializeField] Transform stormPosition;
    public bool stormHit = false;
    public LayerMask Player;

    private void FixedUpdate()
    {
        if (stormHit)
        {
            Collider2D hitPlayer = Physics2D.OverlapBox(stormPosition.position, new Vector2(stormBoxRadiusX, stormBoxRadiusY), 0, Player);
            if (hitPlayer != null)
            {
                hitPlayer.gameObject.GetComponent<PlayerController>().Death();
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
            Gizmos.DrawWireCube(stormPosition.position, new Vector3(stormBoxRadiusX, stormBoxRadiusY));
    }

    public void StormHitPlayer() {
        stormHit = true;
    }
    public void DestroyObject() {
        Destroy(this.gameObject);
    }
}
