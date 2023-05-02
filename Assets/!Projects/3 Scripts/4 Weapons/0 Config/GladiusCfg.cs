using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GladiusCfg : WeaponCfg
{
    public override void Raycast()
    {
        Physics.Linecast(startPoint.position, endPoint.position, out hit, rayMask);
    }
}