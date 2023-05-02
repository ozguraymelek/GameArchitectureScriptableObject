using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Components")]
    public Transform startPoint;
    public Transform endPoint;
        
    [Header("Settings")] 
    public LayerMask rayMask;
    public RaycastHit hit;
    
    public WeaponCfg activeWeaponCfg;
    
    public abstract void ExecuteCfg();
}
