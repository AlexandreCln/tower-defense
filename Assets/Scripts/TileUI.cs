using UnityEngine;

public class TileUI : MonoBehaviour
{
    private Tile targetTile;
    public GameObject ui;

    public void Show(Tile tile)
    {
        targetTile = tile;

        transform.position = targetTile.transform.position;
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}
