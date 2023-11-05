using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellExplainText : MonoBehaviour
{
    private static SpellExplainText instance;
    public static SpellExplainText Instance
    {
        get
        {
            if (instance == null) // instance�� ����ִ�
            {
                var obj = FindObjectOfType<SpellExplainText>(true);
                if (obj != null)
                {
                    instance = obj;                                             // ��ü ã�ƺôµ�? �ֳ�? �װ� ����
                }
                else
                {
                    var newObj = new GameObject().AddComponent<SpellExplainText>(); // ��ü ã�ƺôµ�? ����? ���θ�����
                    instance = newObj;
                }
            }
            return instance; // �Ⱥ���ֳ�? �׳� �״�� ������
        }
    }
    private void Awake()
    {
        var objs = FindObjectsOfType<SpellExplainText>(true);
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }
}
