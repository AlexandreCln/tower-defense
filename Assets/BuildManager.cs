using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Singleton pattern help to use this builder inside each Node object with 
    // a single accessible instance instead of instantiate this class each time.
    // static means accessible without BuildManager instantiation from other class.
    public static BuildManager instance;
    public GameObject standardTurretPrefab;
    private GameObject turretToBuild;

    // Instantiate BuildManager once.
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one BuildManager in scene.");

        instance = this;
    }

    void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
