using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] GameObject GameOver;
    [SerializeField] GameObject RespawnButton;
    [SerializeField] GameObject EndButton;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        GameOver.SetActive(false);
        RespawnButton.SetActive(true);
        EndButton.SetActive(true);
        animator.enabled = false;
    }
    public void BackToMenu() {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
