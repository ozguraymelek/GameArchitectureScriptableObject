using System;
using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using UnityEngine;
using UnityEngine.Pool;

public class GladiusVFX : MonoBehaviour
{
    [SerializeField] private Variable<GladiusVFX> gladiusVfx;
    
    public IObjectPool<GladiusVFX> VfxPool { get; set; }

    public void ReleaseVfx() => VfxPool.Release(this);
}
