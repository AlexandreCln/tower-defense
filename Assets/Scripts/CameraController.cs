using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Attributes")]
    public float panSpeed = 70f;
    public float scrollSpeed = 5f;
    public float minZ = -7;
    public float maxZ = 58;
    public float minX = -25;
    public float maxX = 25;
    public float minZoomY = 10f;
    public float minZoomRotationX = 40f;
    public float panBoarderThickness = 10f;

    private float maxZoomY;
    private float maxZoomRotationX;
    private bool controlCamera = true;

    void Start()
    {
        maxZoomY = transform.position.y;
        maxZoomRotationX = transform.eulerAngles.x;
    }

    void Update()
    {
        HandleRotationZoom();

        if (Input.GetKey(KeyCode.Escape))
            controlCamera = !controlCamera;
            
        if (!controlCamera)
            return;
            
        // HandleKeyboardMove();
        HandleMouseMove();
    }

    void HandleKeyboardMove()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            MoveUp();
        } 
        else if (Input.GetKey(KeyCode.Q))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
    }

    void HandleMouseMove()
    {
        if (Input.mousePosition.y >= Screen.height - panBoarderThickness)
        {
            MoveUp();
        } 
        else if (Input.mousePosition.y <= panBoarderThickness)
        {
            MoveDown();
        }
        else if (Input.mousePosition.x <= panBoarderThickness)
        {
            MoveLeft();
        }
        else if (Input.mousePosition.x >= Screen.width - panBoarderThickness)
        {
            MoveRight();
        }
    }

    void HandleRotationZoom()
    {
        if (scrollSpeed == 0)
            return;

        // Vertical Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        // restrict the zoom to a certain range
        pos.y = Mathf.Clamp(pos.y, minZoomY, maxZoomY);
        transform.position = pos;

        // Camera rotation relative to zoom's height
        if (scroll != 0)
        {
            // zoom height range
            float rangeZoomY = maxZoomY - minZoomY;
            // zoom's rotation amplitude
            float amplitudeZoomRotationX = maxZoomRotationX - minZoomRotationX;
            // calculate ratio between camera's height range and camera's rotation amplitude
            float zoomHeightScalingRatio = amplitudeZoomRotationX / rangeZoomY;

            float heightOfCameraRelativeToRange = transform.position.y - minZoomY;
            float zoomRotationX = minZoomRotationX + heightOfCameraRelativeToRange * zoomHeightScalingRatio;

            transform.rotation = Quaternion.Euler(zoomRotationX, transform.rotation.y, transform.rotation.z); 
        }
    }

    void MoveUp()
    {
        if (transform.position.z >= maxZ)
            return;

        transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
    }
    
    void MoveDown()
    {
        if (transform.position.z <= minZ)
            return;

        transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
    }
    
    void MoveLeft()
    {
        if (transform.position.x <= minX)
            return;

        transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
    }

    void MoveRight()
    {
        if (transform.position.x >= maxX)
            return;

        transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
    }
}
