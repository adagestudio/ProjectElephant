using UnityEngine;
using System.Collections;

public class CoolCamera : MonoBehaviour
{

    [SerializeField] protected Transform target;            // The target object to follow
    [SerializeField] private bool m_AutoTargetPlayer = true;  // Whether the rig should automatically target the player.
    [SerializeField] private float maxHorizontalAngle = 120; //en realidad, [-60,60]
    [SerializeField] private float maxVerticalAngle = 20;
    [SerializeField] private float minVerticalAngle = 5;
    [SerializeField] private float maxZoom = 30;
    [SerializeField] private int minZoom = 1;
    [SerializeField] private Space offsetPositionSpace = Space.Self;
    private CameraSettings cameraDefaultValues = new CameraSettings();

    public float zoomSpeed = 16.0f;
    public float m_angleChangeSmoothFactor = 1;
    public float m_cameraRestoreSmoothFactor = 100f;
    public float horizontalAngle = 0;
    public float verticalAngle = 20;
    public float Zoom = 5f;
    
    //Camera movements gestion
    private Vector3 mouseOrigin;
    private bool isRotating;
    private bool isZooming;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            resetCamera();
        }

        //mouse input checkers
        if (Input.GetMouseButtonDown(1)) {
            isRotating = true;
        }

        if (!Input.GetMouseButton(1))
        {
            isRotating = false;
            StartCoroutine("restoreCamera");
        }

        if (isRotating)
        {
            calcAngles();
        }

        calcZoom();

        if(offsetPositionSpace == Space.Self)
        {
            var position = new Vector3(0,0,0);
            position.z -= Zoom* Mathf.Cos(verticalAngle * Mathf.PI / 180) * Mathf.Cos(horizontalAngle * Mathf.PI / 180);
            position.x += Zoom * Mathf.Cos(verticalAngle * Mathf.PI / 180) * Mathf.Sin(horizontalAngle * Mathf.PI / 180);
            position.y += Zoom * Mathf.Sin(verticalAngle * Mathf.PI / 180);
            transform.position = target.TransformPoint(position);
        }
        else
        {
            var position = target.position;
            position.z -= Zoom * Mathf.Cos(verticalAngle * Mathf.PI / 180) * Mathf.Cos(horizontalAngle * Mathf.PI / 180);
            position.x += Zoom * Mathf.Cos(verticalAngle * Mathf.PI / 180) * Mathf.Sin(horizontalAngle * Mathf.PI / 180);
            position.y += Zoom * Mathf.Sin(verticalAngle * Mathf.PI / 180);
            transform.position = position;
        }

        if (m_AutoTargetPlayer)
            transform.LookAt(target.position + new Vector3(0, 1.5f, 0));
        else
            transform.rotation = target.rotation;
    }

    void calcAngles()
    {
        //var change = mouseOrigin - Input.mousePosition;
        verticalAngle +=  (1 / m_angleChangeSmoothFactor) * Mathf.RoundToInt(Mathf.SmoothStep(verticalAngle, Input.GetAxis("Mouse Y"), 8f));
        verticalAngle = verticalAngle <= maxVerticalAngle ? verticalAngle : maxVerticalAngle;
        verticalAngle = verticalAngle >= minVerticalAngle ? verticalAngle : minVerticalAngle;

        horizontalAngle += (1 / m_angleChangeSmoothFactor) * Mathf.RoundToInt(Mathf.SmoothStep(horizontalAngle, Input.GetAxis("Mouse X"), 8f));
        horizontalAngle = horizontalAngle <= 1f / 2f * maxHorizontalAngle ? horizontalAngle : 1f / 2f * maxHorizontalAngle;
        horizontalAngle = horizontalAngle >= -(1f / 2f * maxHorizontalAngle) ? horizontalAngle : -(1f / 2f * maxHorizontalAngle);
    }

    void calcZoom()
    {
        Zoom = Mathf.SmoothStep(Zoom, Zoom - zoomSpeed * Input.GetAxis("Mouse ScrollWheel"), 16f);
        Zoom = Zoom <= maxZoom ? Zoom : maxZoom;
        Zoom = Zoom >= minZoom ? Zoom : minZoom;
    }

    void resetCamera()
    {
        Zoom = cameraDefaultValues.zoom;
        verticalAngle = cameraDefaultValues.verticalAngle;
        horizontalAngle = cameraDefaultValues.horizontalAngle;
    }

    IEnumerator restoreCamera()
    {
        bool restoreHorizontal = true;
        bool restoreVertical = true;
        //bool restoreZoom = true;
        float smooth = 1f / m_cameraRestoreSmoothFactor;
        while(!isRotating && (restoreHorizontal || restoreVertical))
        {
            if (restoreHorizontal)
            {
                horizontalAngle = Mathf.SmoothStep(horizontalAngle, cameraDefaultValues.horizontalAngle, smooth);
                restoreHorizontal = horizontalAngle != cameraDefaultValues.horizontalAngle;
            }

            if (restoreVertical)
            {
                verticalAngle = Mathf.SmoothStep(verticalAngle, cameraDefaultValues.verticalAngle, smooth);
                restoreVertical = verticalAngle != cameraDefaultValues.verticalAngle;
            }
           /* if (restoreZoom)
            {
                Mathf.SmoothStep(Zoom, cameraDefaultValues., 8f);
            }*/
           yield return null;
        }
    }
}

class CameraSettings
{
    public float zoom = 5;
    public float verticalAngle = 20;
    public float horizontalAngle = 0;
}
