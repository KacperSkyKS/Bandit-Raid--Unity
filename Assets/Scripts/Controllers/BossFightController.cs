using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightController : MonoBehaviour
{
    public Collider2D trigger;
    public Collider2D Wall1;
    public Collider2D Wall2;
    public bool allEnemiesDied = false;
    public int amountOfEnemies;
    public int currentAmountOfEnemies;
    [SerializeField] GameObject BossToken;
    // Start is called before the first frame update
    void Start()
    {
        BossToken.SetActive(false);
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
        if (currentAmountOfEnemies <= 0)
        {
            allEnemiesDied = true;
        }
        if (allEnemiesDied)
        {
            if (Wall1 != null)
            {
                Wall1.enabled = false;
                Wall2.enabled = false;
            }
            trigger.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BossToken.SetActive(true);
            trigger.enabled = false;
            if (Wall1 != null)
            {
                Wall1.enabled = true;
                Wall2.enabled = true;
            }
            this.gameObject.transform.Find("Boss").transform.gameObject.GetComponent<BossAI>().isTriggered = true;

        }
    }
    public void CountEnemies()
    {
        currentAmountOfEnemies = amountOfEnemies;
    }
}
