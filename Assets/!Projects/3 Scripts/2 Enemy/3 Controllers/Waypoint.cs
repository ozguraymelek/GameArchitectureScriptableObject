using System;
using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;


public class Waypoint : MonoBehaviour
{
    public RuntimeSet<Transform> waypoints;
    
    public Variable<bool> isEnemyReached;

    public float radius;
    public LayerMask mask;
    
    public IObjectPool<Waypoint> WaypointPool { get; set; }

    private void FixedUpdate()
    {
        isEnemyReached.Value = Physics.CheckSphere(transform.position, radius, mask);
    }

    public void ReleaseWaypoint() => WaypointPool.Release(this);
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
