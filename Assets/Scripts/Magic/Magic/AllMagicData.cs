using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllMagicData : MonoBehaviour
{
    // ������ ������ ���� �ѹ��� �� ��
    //float[] MobData_Number = { HP,ATK,DEF,CoolTime,MoveSpeed};//���� Stat���� �����´�
    //float[] PlayerData_Number = { HP, ATK, DEF, CoolTime, MoveSpeed }; // ���� ����
    //float[] Return_Number;

    //bool[] DoingSpellType = { isFire, isLiquid, isWind, isEarth }; Ȱ��ȭ �� ����Ÿ�Ե�� ��ų ����
    
    bool[] test = { true, true, false, true };

    void Start()
    {
        
    }

    
    void Update()
    {
        // �̷� ���� MagicAct(MagicName(DoingSpellType));

    }

    private String MagicName(bool[] SpellTp) // �̰� ���̵��?
    {
        String SpellName = "";
        for(int i = 0; i < SpellTp.Length; i++)
        {
            if (SpellTp[i])
            {
                switch (i)
                {
                    case 0:
                        SpellName += "F";
                        break;
                    case 1:
                        SpellName += "L";
                        break;
                    case 2:
                        SpellName += "W";
                        break;
                    case 3:
                        SpellName += "E";
                        break;
                }
            }

        }
        return SpellName;
    }
    /*
    public void MagicAct(String name)
    {
       //������ ���� �Լ��� ���� ����
        if (name == "") { ���� ����}
        else if (name == "F") { }
        else if (name == "L") { }
        else if (name == "W") { }
        else if (name == "E") { }
        else if (name == "FL") { }
        else if (name == "FW") { }
        else if (name == "FE") { }
        else if (name == "LW") { }
        else if (name == "LE") { }
        else if (name == "WE") { }
        else if (name == "FLE") { }
        else if (name == "FWE") { }
        else if (name == "LWE") { }

    }*/
}
