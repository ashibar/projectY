using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabSet", menuName = "Scriptable Object/PrefabSet", order = int.MaxValue)]
public class PrefabSet_so : ScriptableObject
{
    [SerializeField] public List<GameObject> SpellPrefab = new List<GameObject>();
}
