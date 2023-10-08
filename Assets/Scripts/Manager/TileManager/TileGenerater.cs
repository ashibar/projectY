using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerater : MonoBehaviour
{
    [SerializeField] private TileAsset_so tile_asset;
    [SerializeField] private Vector2 position = Vector2.zero;
    [SerializeField] private Vector2 totalsize = new Vector2(2, 2);
    [SerializeField] private float noise = 0.5f;

    [SerializeField] private GameObject originTile;
    [SerializeField] private Transform holder;
    [SerializeField] public TileManager tileManager;

    [SerializeField] private int count = 0;

    private void Awake()
    {
        if (holder == null)
            holder = transform;
    }

    public void SetTile()
    {
        SetTile(position, totalsize);
    }

    public void SetTile(Vector2 pos, Vector2 size)
    {
        // 에셋에서 타일 정보 불러오기
        List<Sprite> base_tile = tile_asset.base_tile;
        List<Sprite> noise_tile = tile_asset.noise_tile;
        float tileSize = tile_asset.tileSize;
        noise = Mathf.Clamp(noise, 0f, 1f);
        
        // 타일 그룹 홀더 오브젝트 생성
        GameObject group = new GameObject("Tile Group " + count++.ToString());
        group.transform.position = (Vector3)pos;
        group.transform.parent = holder;

        // 타일 생성
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                Vector2 relativePos = new Vector2(i - (size.x / 2), j - (size.y / 2)) * tileSize;
                float r = Random.Range(0f, 1f);
                int id;

                GameObject clone = Instantiate(originTile, relativePos + pos, Quaternion.identity, group.transform);
                SpriteRenderer[] sr = clone.GetComponentsInChildren<SpriteRenderer>();

                if (r <= noise)
                {
                    id = Random.Range(0, noise_tile.Count);
                    sr[1].sprite = noise_tile[id];
                }
                else
                {
                    id = Random.Range(0, base_tile.Count);
                    sr[0].sprite = base_tile[id];
                }
            }
        }

        tileManager.tile_group.Add(group);
    }

    public void SetTileAsset(TileAsset_so tile_asset)
    {
        this.tile_asset = tile_asset;
    }

    private void OnValidate()
    {
        noise = Mathf.Clamp(noise, 0f, 1f);
    }
}
