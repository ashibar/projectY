using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLineRenderer : MonoBehaviour
{
    [SerializeField] private List<Sprite> spritelist;
    [SerializeField] private float changeInterval = 0.3f;  // 스프라이트 변경 간격 (초 단위)
    [SerializeField] private int currentSpriteIndex = 0;  // 현재 사용 중인 스프라이트의 인덱스
    [SerializeField] private List<Transform> trList = new List<Transform>();

    private LineRenderer lr;
    private float timer = 0.0f;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        LineRender();
    }

    public void LineRender()
    {
        
        SetPosition();

        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0.0f;
            currentSpriteIndex++;

            if (currentSpriteIndex >= spritelist.Count)
            {
                currentSpriteIndex = 0;
            }

            ChangeSprite(currentSpriteIndex);
        }
    }

    public void AddTransform(params Transform[] tr)
    {
        trList.AddRange(tr);
        lr.positionCount = trList.Count;
    }

    public void SetPosition()
    {
        for (int i = 0; i < trList.Count; i++)
        {
            lr.SetPosition(i, trList[i].position);
        }
    }

    private void ChangeSprite(int index)
    {
        if (spritelist.Count > 0 && index >= 0 && index < spritelist.Count)
        {
            lr.material.SetTexture("_MainTex", spritelist[index].texture);
        }
    }
}
