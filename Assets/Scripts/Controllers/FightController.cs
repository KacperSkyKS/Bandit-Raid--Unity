using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{
    public Collider2D trigger;
    public Collider2D Wall1;
    public Collider2D Wall2;
    public bool allEnemiesDied = false;
    public int amountOfEnemies;
    public int currentAmountOfEnemies;
    // Start is called before the first frame update
    void Start()
    {
        if (Wall1 != null)
        {
            Wall1.enabled = Wall2.enabled = false;
        }
        foreach (Transform child in this.transform)
        {
            amountOfEnemies++;
        }
        CountEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAmountOfEnemies <= 0) {
            allEnemiesDied = true;
        }
        if (allEnemiesDied) {
            if (Wall1!= null)
            {
                Wall1.enabled = false;
                Wall2.enabled = false;
            }
            trigger.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player") {
            trigger.enabled = false;
            if (Wall1 != null)
            {
                Wall1.enabled = true;
                Wall2.enabled = true;
            }
            foreach (Transform child in transform) {
                child.gameObject.GetComponent<EnemyAI>().isTriggered = true;
                child.gameObject.GetComponent<EnemyAI>().currentState = 1;  
            }

        }
    }
    public void CountEnemies() {
        currentAmountOfEnemies = amountOfEnemies;
    }
}
