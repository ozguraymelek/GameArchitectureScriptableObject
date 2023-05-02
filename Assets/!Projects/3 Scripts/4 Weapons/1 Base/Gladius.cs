using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gladius : Weapon
{
    public override void ExecuteCfg()
    {
        activeWeaponCfg = ScriptableObject.CreateInstance<GladiusCfg>();
            
        activeWeaponCfg.startPoint = startPoint;
        activeWeaponCfg.endPoint = endPoint;

        activeWeaponCfg.rayMask = rayMask;
        activeWeaponCfg.hit = hit;
            
        activeWeaponCfg.Raycast();
    }
}
