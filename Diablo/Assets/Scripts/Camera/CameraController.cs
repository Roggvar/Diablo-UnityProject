using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;

    public float pitch = 2f; // angulo da camera
    public float zoomSpeed = 4f; // velocidade do zoom
    public float minZoom = 5f; // zoom minimo
    public float maxZoom = 15f; // zoom maximo
    public float yawSpeed = 300f; // velocidade da rotaçao
    private float currentZoom = 10f; // zoom inicial
    private float currentYaw = 0f; // rotaçao inicial

    private void Update()
    {

        //Zoom da camera
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        //Rotação da camera
        if(Input.GetMouseButton(2))
        { 

            currentYaw -= Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;

        }

    }

    private void LateUpdate()
    {

        //Segue o target
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        //Rotaçao em volta do target
        transform.RotateAround(target.position, Vector3.up, currentYaw);

    }

}
