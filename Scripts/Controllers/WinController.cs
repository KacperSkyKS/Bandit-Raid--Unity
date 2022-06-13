using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    [SerializeField] BossAI bossai;
    [SerializeField] Canvas canvas;
    [SerializeField] Animator animator;
    [SerializeField] PointsController points;
    [SerializeField] Text scoretext;

    public float timer = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossai.isDead) {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else {
                scoretext.text = "Score " + points.currentPoints;
                canvas.enabled = true;
                animator.enabled = true;
            }
        }
        
    }
}
