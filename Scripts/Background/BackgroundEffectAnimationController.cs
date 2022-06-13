using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEffectAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    private void Awake()
    {
        animator.enabled = false;
    }
    private void OnBecameInvisible()
    {
        animator.enabled = false;   
    }
    private void OnBecameVisible()
    {
        animator.enabled = true;
    }
}
