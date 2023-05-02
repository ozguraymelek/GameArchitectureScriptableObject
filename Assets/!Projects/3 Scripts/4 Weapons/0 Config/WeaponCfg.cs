using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class WeaponCfg : ScriptableObject
{
    [Header("Components")]
    public Transform startPoint;
    public Transform endPoint;

    [Header("Settings")] 
    public LayerMask rayMask;
    public RaycastHit hit;
    
    public abstract void Raycast();

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startPoint.position, endPoint.position);
    }
}
