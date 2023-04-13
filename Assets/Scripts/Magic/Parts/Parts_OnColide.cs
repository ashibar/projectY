using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_OnColide : Parts
{
    [SerializeField] private List<GameObject> origins = new List<GameObject>();
    
    public override void Applier(GameObject proj, Stat_Spell stat, Collider2D collision)
    {
        
        CollisionProcess(proj, stat, collision);
        CloningProcess(proj, stat, collision);
    }

    protected virtual void CollisionProcess(GameObject proj, Stat_Spell stat, Collider2D collision)
    {
        // ���⿡ ����ü�� �ε����� �� �Լ�
    }

    protected virtual void CloningProcess(GameObject proj, Stat_Spell stat, Collider2D collision)
    {
        // ���⿡ ����ü�� �ε�ģ �� �߰� ����ü ���� �Լ�
    }
}
