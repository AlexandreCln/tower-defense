using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // makes points accessibles without class instantiation
    public static Transform[] points;

    void Awake ()
    {
        points = new Transform[transform.childCount];

        for (int i=0; i<points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
