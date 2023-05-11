using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMain : MonoBehaviour
{
    public Transform[] spawnpoint;
    public SpawnManager[] spawnManager;
    float spawntimer;
    public Vector2 spawnpointvector;

    void Awake()
    {
        spawnManager = GetComponentsInParent<SpawnManager>();
        spawnpoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        spawntestpoint();
        
    }
    void spawntestpoint()
    {
        
        spawnpointvector= spawnpoint[Random.Range(1,spawnpoint.Length)].position;
        Debug.Log(spawnpointvector);
    }
}
