using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header("Attributes")]
    public Color hoverColor;
    public Color activeColor;
    public Color modifyColor;

    private Vector3 initialPos;
    private Color initialColor;
    private Renderer rend;
    private GameObject construction;
    private BuildManager buildManager;
    private bool isActive = false;

    void Start()
    {
        rend = GetComponent<Renderer>();

        initialPos = transform.position;
        initialColor = rend.material.color;
        buildManager = BuildManager.instance;
    }
    void OnMouseEnter()
    {
        if (
            !buildManager.IsConstructionPrefab() ||
            EventSystem.current.IsPointerOverGameObject()
        )
            return;

        if (construction != null)
        {
            // rend.material.color = modifyColor;
        }
        else
        {
            ActiveNode();
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
            !buildManager.IsConstructionPrefab() ||
            EventSystem.current.IsPointerOverGameObject()
        )
            return;
        
        if (construction != null)
        {
            Debug.Log("Must implement modification.");
        }
        else
        {
            ActiveNode();

            // Build a turret on it
            GameObject constructionPrefab = buildManager.GetConstructionPrefab();
            construction = (GameObject)Instantiate(constructionPrefab, transform.position, transform.rotation);
        }
    }

    void ActiveNode()
    {
        if (isActive)
            return;

        isActive = true;

        rend.material.color = hoverColor;
        transform.Translate(Vector3.up * 0.5f, Space.World);
    }

    void ResetNode()
    {
        isActive = false;
        transform.position = initialPos;
        rend.material.color = initialColor;
    }
}
