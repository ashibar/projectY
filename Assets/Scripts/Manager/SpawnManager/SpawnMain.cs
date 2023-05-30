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

        // 계산된 위치로 이동
        spawnpointvector = randomPosition;
        return spawnpointvector;
    }


    private Vector2 CircleVector(Vector2 center, float radius)
    {
        int pointCount = 100; // 좌표를 얼마나 세밀하게 계산할지 결정하는 포인트 개수
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

    private Vector2 SpawnStraight(Vector2 center, float amount,bool isWidth ,bool isTop, bool isLeft)
    {
        float screenWidth = Screen.width;
        float pointDivWidth = screenWidth / (amount - 1);
        float screenHight = Screen.height;
        float pointDivHeight = screenHight / (amount - 1);

        Vector2 point = new();
        if(isWidth)
        {
            for (int i = 0; i < amount; i++)
            {
                float x = i * pointDivWidth;
                float y = (isTop) ? Screen.height : -Screen.height;
                point = new Vector2(x, y);
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                float x = (isLeft) ? Screen.width : -Screen.width;
                float y = i * pointDivHeight;
                point = new Vector2(x, y);
            }
        }
        return point;
    }
}
