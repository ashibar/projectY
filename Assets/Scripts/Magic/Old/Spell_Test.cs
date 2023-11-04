using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Spell_Test : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform a;
    [SerializeField] private Transform b;

    [SerializeField] private List<Sprite> spritelist;

    private int currentSpriteIndex = 0;  // ���� ��� ���� ��������Ʈ�� �ε���
    public float changeInterval = 0.3f;  // ��������Ʈ ���� ���� (�� ����)
    private float timer = 0.0f;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        
        if (a != null && b != null)
        {
            lr.SetPosition(0, a.position);
            lr.SetPosition(1, b.position);
            //lr.material.SetTexture("_MainTex", spritelist[0].texture);

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

        void ChangeSprite(int index)
        {
            if (spritelist.Count > 0 && index >= 0 && index < spritelist.Count)
            {
                lr.material.SetTexture("_MainTex", spritelist[index].texture);
            }
        }
    }
}
