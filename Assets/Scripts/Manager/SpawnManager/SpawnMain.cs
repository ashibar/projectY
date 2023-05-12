using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnMain : MonoBehaviour
{
    public Transform[] spawnpoint;
    public SpawnManager[] spawnManager;
    float spawntimer;
    public Vector2 spawnpointvector;
    public float range;

    void Awake()
    {
        spawnManager = GetComponentsInParent<SpawnManager>();
        spawnpoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        //spawntestpoint();
        
    }
    public Vector2 spawntestpoint()
    {
        
        spawnpointvector= spawnpoint[Random.Range(1,spawnpoint.Length)].position;
        Debug.Log(spawnpointvector);
        return spawnpointvector;
    }
    public Vector2 SpawnRangePoint(float _range)
    {
        range = _range;
        Vector2 mainPos = transform.position;
        Vector3 randomOffset = Random.insideUnitSphere * range;
        Vector2 randomPosition = mainPos + (Vector2)randomOffset;

        // 계산된 위치로 이동
        spawnpointvector = randomPosition;
        return spawnpointvector;
    }
}
