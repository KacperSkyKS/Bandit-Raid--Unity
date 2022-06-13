using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonInGameController : MonoBehaviour
{
    [SerializeField] GameObject GameOver;
    [SerializeField] Animator animator;
    public void GameOverEnabled()
    {
        GameOver.SetActive(true);
        animator.enabled = true;
        Time.timeScale = 1;
    }
}
