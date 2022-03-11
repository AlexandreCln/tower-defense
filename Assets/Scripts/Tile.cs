using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    private Vector3 initialPos;
    private Color initialColor;
    private Renderer rend;
    private BuildManager buildManager;
    private bool isActive = false;

    [Header("Attributes")]
    public Color activeColor;
    public Color alarmingColor;

    [Header("Optionnal")]
    public GameObject construction;

    Vector3 positionOffset = new Vector3(0, 0.5f, 0);

    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();

        initialPos = transform.position;
        initialColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {   
        return transform.position + positionOffset;
    }

    void OnMouseOver()
    {
        if (
            false == buildManager.CanBuild ||
            EventSystem.current.IsPointerOverGameObject()
        )
            return;

        if (construction != null)
        {
            // rend.material.color = modifyColor;
        }
        else
        {
            if (buildManager.CanBuy)
            {
                ActiveTile();
            }
            else
            {
                AlarmingTile();
            }
        }
    }

    void OnMouseExit()
    {
        if (construction != null)
        {
            ActiveTile();
        }
        else
        {
            ResetTile();
        }
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("IsPointerOverGameObject");
            return;
        }

        if (construction != null)
        {
            buildManager.SetSelectedTile(this);
        }
        else if (buildManager.CanBuild && buildManager.CanBuy)
        {
            ActiveTile();
            buildManager.BuildConstructionOn(this);
        }
    }

    void ActiveTile()
    {
        if (isActive)
            return;

        isActive = true;

        rend.material.color = activeColor;
        transform.Translate(Vector3.up * positionOffset.y, Space.World);
    }

    void AlarmingTile()
    {
        rend.material.color = alarmingColor;
    }

    void ResetTile()
    {
        isActive = false;
        transform.position = initialPos;
        rend.material.color = initialColor;
    }
}
