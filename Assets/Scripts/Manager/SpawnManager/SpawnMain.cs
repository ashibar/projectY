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
        Vector2 randomPosition = CircleVector(mainPos, _range);

        // ���� ��ġ�� �̵�
        spawnpointvector = randomPosition;
        return spawnpointvector;
    }
    private Vector2 CircleVector(Vector2 center, float radius)
    {
        int pointCount = 100; // ��ǥ�� �󸶳� �����ϰ� ������� �����ϴ� ����Ʈ ����
        Vector2 point = new Vector2();
        for (int i = 0; i < pointCount; i++)
        {
            float angle = Random.Range(0f, 2f * Mathf.PI); ;
            float x = center.x + radius * Mathf.Cos(angle);
            float y = center.y + radius * Mathf.Sin(angle);

             point = new Vector2(x, y);

            
        }

        return point;
    }
}
