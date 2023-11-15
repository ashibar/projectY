using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Camera : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        CameraZoom();
    }

    private void CameraZoom()
    {
        float value = Input.GetAxisRaw("Mouse ScrollWheel");
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + value * -10f, 10f, 20f);
    }
}
