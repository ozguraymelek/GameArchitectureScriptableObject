using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignedAngleExample : MonoBehaviour
{
    public Transform targetPoint;
    public Rigidbody rb;
    
    public Vector3 targetDirection;
    public Vector3 playerCalculateVector;

    public float angle;

    private void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetDirection = targetPoint.transform.position - transform.position;
            
            playerCalculateVector = transform.forward;
            
            angle = Vector3.SignedAngle(targetDirection, 
                playerCalculateVector, Vector3.up);
            
            Debug.Log($"Angle: {angle}");
        }

        else if (Input.GetKey(KeyCode.A))
        {
            var targetRot = Quaternion.Euler(0, -angle
                , 0);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 2f * Time.fixedDeltaTime);

            rb.velocity = targetDirection * (15f * Time.fixedDeltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        Gizmos.DrawLine(targetPoint.transform.position, transform.position);
        
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
