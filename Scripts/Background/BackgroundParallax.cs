using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private float parallaxEfectMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEfectMultiplier, deltaMovement.y, transform.position.z);
        lastCameraPosition = cameraTransform.position;
    }
}
