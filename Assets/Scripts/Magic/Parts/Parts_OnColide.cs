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
        // 여기에 투사체가 부딪쳤을 때 함수
    }

    protected virtual void CloningProcess(Applier_parameter para)
    {
        // 여기에 투사체가 부딪친 후 추가 투사체 생성 함수
    }
}
