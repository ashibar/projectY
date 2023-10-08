using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_ColliderStatApplier : Parts_OnColide
{
    [SerializeField] private bool isPenetrate;
    [SerializeField] private List<Collider2D> unit_already_collides = new List<Collider2D>();

    protected override void CollisionProcess(Applier_parameter para)
    {
        base.CollisionProcess(para);
        PenetrateProcess(para);
    }

    private void PenetrateProcess(Applier_parameter para)
    {
        bool isInList = false;
        // 관통형이 아니면 데미지를 주고 삭제
        if (!isPenetrate)
        {
            para.Collision.GetComponent<Unit>().stat.Hp_current -= para.Stat.Spell_DMG;
            Destroy(para.Proj);
        }
        // 관통형일 경우
        else
        {
            // 이미 부딪친 충돌체인지 확인
            foreach (Collider2D col in unit_already_collides)
                if (para.Collision == col)
                {                    
                    isInList = true;
                    break;
                }
            
            // 처음 부딪친 충돌체면 데미지를 주고 리스트에 넣음
            if (!isInList)
            {
                para.Collision.GetComponent<Unit>().stat.Hp_current -= para.Stat.Spell_DMG;
                unit_already_collides.Add(para.Collision);
            }
        }
    }
}
