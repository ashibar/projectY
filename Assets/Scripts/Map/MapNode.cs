using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    [SerializeField] public int index;
    [SerializeField] public bool isAccessable;
    [SerializeField] public MapNode left;
    [SerializeField] public MapNode right;
    [SerializeField] public MapNode up;
    [SerializeField] public MapNode down;

    [SerializeField] private List<Sprite> spritelist;

    [SerializeField] private float changeInterval = 0.3f;  // ��������Ʈ ���� ���� (�� ����)
    [SerializeField] private int currentSpriteIndex = 0;  // ���� ��� ���� ��������Ʈ�� �ε���
    private float timer = 0.0f;

    private SpriteRenderer spriteRenderer;
    private LineRenderer lr;
    [SerializeField] private List<Transform> trList = new List<Transform>();

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();
        //trList.Add(transform);
        //if (left != null) trList.Add(left.transform);
        //if (right != null) trList.Add(right.transform);
        //if (up != null) trList.Add(up.transform);
        //if (down != null) trList.Add(down.transform);
        lr.positionCount = trList.Count;
        lr.sortingLayerName = "NPC";
    }

    private void Update()
    {
        Color c = spriteRenderer.color;
        spriteRenderer.color = new Color(c.r, c.g, c.b, isAccessable ? 1f : 0.5f);

        LineRender();
    }

    private void LineRender()
    {
        SetPostion();

        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            // ��������Ʈ ���� ���ݿ� �������� ��
            timer = 0.0f;  // Ÿ�̸� �ʱ�ȭ

            // ���� ��� ���� ��������Ʈ�� ����
            currentSpriteIndex++;

            // ��������Ʈ ����Ʈ�� ���� �����ϸ� ó�� ��������Ʈ�� ���ư�
            if (currentSpriteIndex >= spritelist.Count)
            {
                currentSpriteIndex = 0;
            }

            // ��������Ʈ�� ����� ��������Ʈ�� ����
            ChangeSprite(currentSpriteIndex);
        }
    }

    private void SetPostion()
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
