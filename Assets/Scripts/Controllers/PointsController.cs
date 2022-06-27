using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour
{
    public Text pointText;
    [SerializeField] public BossAI bossai;
    private int startPoints=10000;
    public int currentPoints;
    public static float timer = 0.25f;
    public float currentTimer = timer;

    void Start()
    {
        currentPoints = startPoints;
        pointText.text = currentPoints.ToString();        
    }
    void Update()
    {
        if (!bossai.isDead)
        {
            ChangePoints();
        }      
    }
    public void ChangePoints() {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        else {
            if (currentPoints > 0)
            {
                currentPoints -= 1;
            }
            pointText.text = currentPoints.ToString();
            currentTimer = timer;
        }
    }
    public void AddPoints(int points) {
        currentPoints += points;
        pointText.text = currentPoints.ToString();
    }
}
