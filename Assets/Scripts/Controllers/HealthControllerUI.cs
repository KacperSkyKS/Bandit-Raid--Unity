using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControllerUI : MonoBehaviour
{
    [SerializeField] BossAI bossAI;
    [SerializeField] Image [] hearths;
    [SerializeField] Sprite emptyHearth;
    [SerializeField] Sprite fullHearth;

    void Update()
    {
        for (int i = bossAI.maxHP-1; i > bossAI.currentHP-1;i--) {
            hearths[i].sprite = emptyHearth;
        }
        for (int i = 1; i < bossAI.currentHP; i++) {
            hearths[i].sprite = fullHearth;
        }
        
    }
}
