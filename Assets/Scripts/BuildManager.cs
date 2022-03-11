using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private ConstructionBlueprint constructionBlueprint;
    private Tile selectedTile;

    // Singleton pattern help to use this builder inside each Tile object with 
    // a single accessible instance instead of instantiate this class each time.
    // static means accessible without BuildManager instantiation from other class.
    public static BuildManager instance;
    public GameObject buildEffect;
    public TileUI tileUI;

    // Instantiate BuildManager once.
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one BuildManager in scene.");

        instance = this;
    }

    public void SetSelectedTile(Tile tile)
    {
        if (tile == selectedTile)
        {
            DeselectTile();
            return;
        }

        selectedTile = tile;
        
        constructionBlueprint = null;
        tileUI.Show(tile);
    }

    void DeselectTile()
    {
        // ensure there is no tile selected while we select a construction
        selectedTile = null;
        tileUI.Hide();
    }

    public void SetConstructionBlueprint(ConstructionBlueprint blueprint)
    {
        constructionBlueprint = blueprint;
        DeselectTile();
    }

    public bool CanBuild 
    { 
        get
        {
            return constructionBlueprint != null;
        }
    }

    public bool CanBuy 
    { 
        get => PlayerStats.Money >= constructionBlueprint.cost; 
    }

    public void BuildConstructionOn(Tile tile)
    {
        if (PlayerStats.Money < constructionBlueprint.cost)
            return;

        PlayerStats.Money -= constructionBlueprint.cost;

        GameObject construction = Instantiate(constructionBlueprint.prefab, tile.GetBuildPosition(), Quaternion.identity);
        tile.construction = construction;

        GameObject effect= Instantiate(buildEffect, tile.GetBuildPosition(), Quaternion.identity);
        float effectDuration = buildEffect.GetComponent<ParticleSystem>().main.duration;
        Destroy(effect, effectDuration);
    }
}