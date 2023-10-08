using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnTet", menuName = "Scriptable Object/SpawnManager", order = int.MaxValue)]
public class SpawnEvent : ScriptableObject
{
    [SerializeField] private List<EventParams> events = new List<EventParams>();
    public List<EventParams> Events { get => events; set => events = value; }
}
