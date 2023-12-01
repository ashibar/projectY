using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private TileGenerater tileGenerater;
    [SerializeField] private TilePositionSetter tilePositionSetter;
    [SerializeField] public List<GameObject> tile_group = new List<GameObject>();

    [SerializeField] private float distance = 5f;
    [SerializeField] private bool distanceByCamera = false;
    [SerializeField] public bool isActive = false;

    private void Awake()
    {
        tileGenerater = GetComponentInChildren<TileGenerater>();
        tilePositionSetter = GetComponentInChildren<TilePositionSetter>();

        tileGenerater.tileManager = this;
        tilePositionSetter.tileManager = this;
    }

    private void Start()
    {
        if (isActive)
        {
            //Debug.Log(Camera.main.orthographicSize);
            if (distanceByCamera)
                distance = Camera.main.orthographicSize * 3.8f;
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    tileGenerater.SetTile(Camera.main.transform.position + new Vector3(i, j) * distance, new Vector2(distance, distance)); 
        }
    }

    private void Update()
    {
        if (isActive)
        {
            if (distanceByCamera)
                distance = Camera.main.orthographicSize * 3.8f;
            tilePositionSetter.distance = distance;

            List<Vector2> tilePos = new List<Vector2>(tilePositionSetter.SetTilePosition());
            if (tilePos.Count > 0)
                foreach (Vector2 pos in tilePos)
                    tileGenerater.SetTile(pos, new Vector2(distance, distance));

            //if (Input.GetKeyDown(KeyCode.G))
            //    tileGenerater.SetTile(); 
        }
    }
}
