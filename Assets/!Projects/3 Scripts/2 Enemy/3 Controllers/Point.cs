using System;
using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;


public class Point : MonoBehaviour
{
    public Variable<bool> isEnemyReached;

    [Header("Settings")]
    public float radius;
    public LayerMask mask;
    
    private void FixedUpdate()
    {
        isEnemyReached.Value = Physics.CheckSphere(transform.position, radius, mask);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}