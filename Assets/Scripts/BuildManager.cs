using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Singleton pattern help to use this builder inside each Node object with 
    // a single accessible instance instead of instantiate this class each time.
    // static means accessible without BuildManager instantiation from other class.
    public static BuildManager instance;

    [Header("Prefabs")]
    public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefab;

    private GameObject constructionPrefab;

    // Instantiate BuildManager once.
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one BuildManager in scene.");

        instance = this;
    }

    public GameObject GetConstructionPrefab() => constructionPrefab;

    public GameObject SetConstructionPrefab(GameObject turretPrefab) => constructionPrefab = turretPrefab;

    public bool IsConstructionPrefab() => constructionPrefab != null;
}
