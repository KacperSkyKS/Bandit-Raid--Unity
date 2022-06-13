using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{

    public GameObject RespawnController;
    public int checkpointNumber;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RespawnController.GetComponent<RespawnController>().currentCheckpoint = checkpointNumber;    
    }
}
