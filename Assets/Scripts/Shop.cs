using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public ConstructionBlueprint standardTurret;
    public ConstructionBlueprint missileLauncher;

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
}
