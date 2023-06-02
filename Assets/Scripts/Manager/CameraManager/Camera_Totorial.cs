using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Totorial : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float x;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam.transform.position = new Vector3(x, cam.transform.position.y, cam.transform.position.z);
        }
    }
}
