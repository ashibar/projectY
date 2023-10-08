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
        // �������� �ƴϸ� �������� �ְ� ����
        if (!isPenetrate)
        {
            para.Collision.GetComponent<Unit>().stat.Hp_current -= para.Stat.Spell_DMG;
            Destroy(para.Proj);
        }
        // �������� ���
        else
        {
            // �̹� �ε�ģ �浹ü���� Ȯ��
            foreach (Collider2D col in unit_already_collides)
                if (para.Collision == col)
                {                    
                    isInList = true;
                    break;
                }
            
            // ó�� �ε�ģ �浹ü�� �������� �ְ� ����Ʈ�� ����
            if (!isInList)
            {
                para.Collision.GetComponent<Unit>().stat.Hp_current -= para.Stat.Spell_DMG;
                unit_already_collides.Add(para.Collision);
            }
        }
    }
}
