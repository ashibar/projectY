using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePositionSetter : MonoBehaviour
{
    [SerializeField] private Camera main_cam;
    [SerializeField] private Transform cam_tr;
    [SerializeField] public float distance = 10;

    [SerializeField] private Vector3 pos_post;

    [SerializeField] public TileManager tileManager;

    private void Awake()
    {
        main_cam = Camera.main;
    }

    private void Start()
    {
        cam_tr = main_cam.transform;
        pos_post = cam_tr.position;
    }

    public Vector2[] SetTilePosition()
    {
        Vector3 p = cam_tr.position;
        Vector2[] vecList = new Vector2[] { };
        if (p.x > pos_post.x + distance)
        {
            vecList = new Vector2[] {
                new Vector2(pos_post.x + distance * 2, pos_post.y + distance * 1),
                new Vector2(pos_post.x + distance * 2, pos_post.y + distance * 0),
                new Vector2(pos_post.x + distance * 2, pos_post.y + distance * -1),
            };
            List<GameObject> list = tileManager.tile_group;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Vector3 pos = list[i].transform.position;
                GameObject obj = list[i];
                if (pos.x < pos_post.x)
                {
                    list.RemoveAt(i);
                    Destroy(obj);
                }
            }
            pos_post = new Vector2(pos_post.x + distance, pos_post.y);            
        }            
        else if (p.x < pos_post.x - distance)
        {
            vecList = new Vector2[] {
                new Vector2(pos_post.x - distance * 2, pos_post.y + distance * 1),
                new Vector2(pos_post.x - distance * 2, pos_post.y + distance * 0),
                new Vector2(pos_post.x - distance * 2, pos_post.y + distance * -1),
            };
            List<GameObject> list = tileManager.tile_group;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Vector3 pos = list[i].transform.position;
                GameObject obj = list[i];
                if (pos.x > pos_post.x)
                {
                    list.RemoveAt(i);
                    Destroy(obj);
                }
            }
            pos_post = new Vector2(pos_post.x - distance, pos_post.y);
        }
        else if (p.y > pos_post.y + distance)
        {
            vecList = new Vector2[] {
                new Vector2(pos_post.x + distance * -1, pos_post.y + distance * 2),
                new Vector2(pos_post.x + distance * 0, pos_post.y + distance * 2),
                new Vector2(pos_post.x + distance * 1, pos_post.y + distance * 2),
            };
            List<GameObject> list = tileManager.tile_group;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Vector3 pos = list[i].transform.position;
                GameObject obj = list[i];
                if (pos.y < pos_post.y)
                {
                    list.RemoveAt(i);
                    Destroy(obj);
                }
            }
            pos_post = new Vector2(pos_post.x, pos_post.y + distance);
        }
        else if (p.y < pos_post.y - distance)
        {
            vecList = new Vector2[] {
                new Vector2(pos_post.x + distance * -1, pos_post.y + distance * -2),
                new Vector2(pos_post.x + distance * 0, pos_post.y + distance * -2),
                new Vector2(pos_post.x + distance * 1, pos_post.y + distance * -2),
            };
            List<GameObject> list = tileManager.tile_group;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Vector3 pos = list[i].transform.position;
                GameObject obj = list[i];
                if (pos.y > pos_post.y)
                {
                    list.RemoveAt(i);
                    Destroy(obj);
                }
            }
            pos_post = new Vector2(pos_post.x, pos_post.y - distance);
        }
        return vecList;
    }
}
