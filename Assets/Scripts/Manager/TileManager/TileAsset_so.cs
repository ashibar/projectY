using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Tile Asset", menuName = "Scriptable Object/Tile Asset SO", order = int.MaxValue)]
public class TileAsset_so : ScriptableObject
{
    [SerializeField] public List<Sprite> base_tile = new List<Sprite>();
    [SerializeField] public List<Sprite> noise_tile = new List<Sprite>();
    [SerializeField] public float tileSize = 1f;
}
