using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Range(0f, 1f)]
    public float parallaxSpeed = 0f;
    private Transform cameraTransform;
    private float Xant;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        Xant = cameraTransform.position.x;
    }
    void Update()
    {
        float deltaCamera = cameraTransform.position.x - Xant;
        if (deltaCamera > 0)
        {
            Vector3 newPos = transform.position;
            newPos.x += parallaxSpeed * deltaCamera;
        }
    }
    
}
