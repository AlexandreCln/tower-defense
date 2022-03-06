using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    
    public void PurchaseStandardTurret()
    {
        buildManager.SetConstructionPrefab(buildManager.standardTurretPrefab);
    }
    
    public void PurchaseMissileLauncher()
    {
        buildManager.SetConstructionPrefab(buildManager.missileLauncherPrefab);
    }
}
