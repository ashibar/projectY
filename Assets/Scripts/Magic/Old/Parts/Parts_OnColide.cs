using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_OnColide : Parts
{
    [SerializeField] private List<GameObject> origins = new List<GameObject>();
    
    public override void Applier(Applier_parameter para)
    {
        
        CollisionProcess(para);
        CloningProcess(para);
    }

    protected virtual void CollisionProcess(Applier_parameter para)
    {
        // ���⿡ ����ü�� �ε����� �� �Լ�
    }

    protected virtual void CloningProcess(Applier_parameter para)
    {
        // ���⿡ ����ü�� �ε�ģ �� �߰� ����ü ���� �Լ�
    }
}
