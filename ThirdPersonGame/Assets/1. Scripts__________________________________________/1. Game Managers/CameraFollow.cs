//Copyright Ex/IO 2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraFollow : MonoBehaviour
{
    Transform playerTransform;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;



    private void Start()
    {
        playerTransform = GameManager.instance.playerManager.transform;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
