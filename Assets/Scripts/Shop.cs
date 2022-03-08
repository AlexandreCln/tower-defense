using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public ConstructionBlueprint standardTurret;
    public ConstructionBlueprint missileLauncher;
    public ConstructionBlueprint laserBeamer;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    
    public void SelectStandardTurret()
    {
        buildManager.SetConstructionBlueprint(standardTurret);
    }
    
    public void SelectMissileLauncher()
    {
        buildManager.SetConstructionBlueprint(missileLauncher);
    }
    
    public void SelectLaserBeamer()
    {
        buildManager.SetConstructionBlueprint(laserBeamer);
    }
}
