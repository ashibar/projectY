using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> base_tile = new List<GameObject>();
    [SerializeField] private List<GameObject> noise_tile = new List<GameObject>();
    [SerializeField] private Vector2 size = new Vector2(2, 2);
    [SerializeField] private float noise = 0.5f;

    [SerializeField] private Transform holder;

    private void Awake()
    {
        holder = transform;
    }

    private void SetTile()
    {

    }
}
