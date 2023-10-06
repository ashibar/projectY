using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Spawn Container", menuName = "Container/Spawn Container", order = 0)]
public class SpawnInfoContainer : ScriptableObject
{
    [SerializeField] public List<SpawnInfo> spawnInfo = new List<SpawnInfo>();
}
