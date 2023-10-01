using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Animation SO", menuName = "Scriptable Object/Projectile Animation SO", order = int.MaxValue)]
public class Projectile_Animation_so : ScriptableObject
{
    [SerializeField] public List<Sprite> sprites = new List<Sprite>();
    [SerializeField] public float fixedMS = 1000f;
    [SerializeField] public float speed = 1f;
}
