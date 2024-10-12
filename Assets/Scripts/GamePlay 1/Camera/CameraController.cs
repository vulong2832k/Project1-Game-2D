using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] protected Transform targetPlayer;
    [SerializeField] protected Vector3 offset;
    [SerializeField] protected float smoothSpeed;

    private void LateUpdate()
    {
        if (targetPlayer == null) return;

        Vector3 desiredPosition = targetPlayer.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
