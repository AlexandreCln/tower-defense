using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header("Attributes")]
    public Color activeColor;
    public Color alarmingColor;
    public Color hoverColor;
    public Color modifyColor;

    [Header("Optionnal")]
    public GameObject construction;

    private Vector3 initialPos;
    private Color initialColor;
    private Renderer rend;
    private BuildManager buildManager;
    private bool isActive = false;

    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();

        initialPos = transform.position;
        initialColor = rend.material.color;

        // if (construction != null)
        // {
        //     ActiveNode();
        //     buildManager.SetConstructionBlueprint(construction);
        //     buildManager.BuildConstructionOn(this);
        // }
    }
    void OnMouseEnter()
    {
        if (
            !buildManager.CanBuild ||
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
                ActiveNode();
            }
            else
            {
                AlarmingNode();
            }
        }
    }

    void OnMouseExit()
    {
        if (construction != null)
        {
            ActiveNode();
        }
        else
        {
            ResetNode();
        }
    }

    void OnMouseDown()
    {
        if (
            !buildManager.CanBuild ||
            EventSystem.current.IsPointerOverGameObject()
        )
            return;
        
        if (construction != null)
        {
            Debug.Log("Must implement modification.");
            return;
        }
        else if (buildManager.CanBuy)
        {
            ActiveNode();
            buildManager.BuildConstructionOn(this);
        }
    }

    void ActiveNode()
    {
        if (isActive)
            return;

        isActive = true;

        rend.material.color = activeColor;
        transform.Translate(Vector3.up * 0.5f, Space.World);
    }

    void AlarmingNode()
    {
        rend.material.color = alarmingColor;
    }

    void ResetNode()
    {
        isActive = false;
        transform.position = initialPos;
        rend.material.color = initialColor;
    }
}
