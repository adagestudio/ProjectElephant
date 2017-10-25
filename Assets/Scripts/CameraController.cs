using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    private float currentZoom = 10f;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float yawSpeed = 100f;
    public float currentYaw = 0f;

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; //El efecto es invertido
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom); //Fuerza el zoom entre los umbrales

        if (Input.GetMouseButton(1))
        {
            currentYaw -= Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime; //Fija velocidad de rotación de cámara
        }
    }

    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch); //Mira a los pies (target.position) + una altura
        transform.RotateAround(target.position, Vector3.up, currentYaw); //Rota la cámara alrededor del target
    }
}
