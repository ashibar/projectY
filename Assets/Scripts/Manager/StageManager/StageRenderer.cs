using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRenderer : MonoBehaviour
{
    [SerializeField] private List<GameObject> tile_original = new List<GameObject>();
    [SerializeField] private Vector2 map_size;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector2 start_pos;
    private void Awake()
    {
        for (int i = 0; i < map_size.x; i++)
        {
            for (int j = 0; j < map_size.y; j++)
            {
                GameObject clone = Instantiate(tile_original[0], (Vector2)cam.transform.position + new Vector2(i, j) - (map_size / 2), Quaternion.identity);
            }
        }
    }
}
