using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color activeColor;
    public Color modifyColor;

    private Color initialColor;
    private Renderer rend;
    private GameObject turret;

    void Start()
    {
        rend = GetComponent<Renderer>();
        initialColor = rend.material.color;
    }
    void OnMouseEnter()
    {
        if (turret != null)
        {
            rend.material.color = modifyColor;
        }
        else
        {
            rend.material.color = hoverColor;
            RaiseTheNode();
        }
    }

    void OnMouseExit()
    {
        if (turret != null)
        {
            rend.material.color = activeColor;
        }
        else
        {
            rend.material.color = initialColor;
            LowerTheNode();
        }
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Must implement modification.");
        }
        else
        {
            rend.material.color = activeColor;

            // Build a turret on it
            GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
            turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
        }
    }

    void RaiseTheNode()
    {
        transform.Translate(Vector3.up * 0.5f, Space.World);
    }

    void LowerTheNode()
    {
        transform.Translate(Vector3.down * 0.5f, Space.World);
    }
}
