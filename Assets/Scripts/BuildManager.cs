using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Singleton pattern help to use this builder inside each Node object with 
    // a single accessible instance instead of instantiate this class each time.
    // static means accessible without BuildManager instantiation from other class.
    public static BuildManager instance;

    public GameObject buildEffect;

    private ConstructionBlueprint constructionBlueprint;

    // Instantiate BuildManager once.
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one BuildManager in scene.");

        instance = this;
    }

    public void SetConstructionBlueprint(ConstructionBlueprint construction) => constructionBlueprint = construction;

    public bool CanBuild { 
        get => constructionBlueprint != null; 
    }

    public bool CanBuy { 
        get => PlayerStats.Money >= constructionBlueprint.cost; 
    }

    public void BuildConstructionOn(Node node)
    {
        if (PlayerStats.Money < constructionBlueprint.cost)
            return;

        PlayerStats.Money -= constructionBlueprint.cost;

        GameObject construction = Instantiate(constructionBlueprint.prefab, node.transform.position, Quaternion.identity);
        node.construction = construction;

        GameObject effect= Instantiate(buildEffect, node.transform.position, Quaternion.identity);
        float effectDuration = buildEffect.GetComponent<ParticleSystem>().main.duration;
        Destroy(effect, effectDuration);
    }
}
