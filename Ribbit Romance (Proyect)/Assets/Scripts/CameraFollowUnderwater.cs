using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowUnderwater : MonoBehaviour
{
    public Vector3 offset;
    public float smoothTime;
    public Vector3 camVelocity;
  

    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset + PlayerMovWater.camOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref camVelocity, smoothTime);

    }

    
    
}
