using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    // Start is called before the first frame update
    public void DisableObject() {
        this.gameObject.SetActive(false);
    }
}
