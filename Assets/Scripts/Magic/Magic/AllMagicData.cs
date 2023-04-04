using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllMagicData : MonoBehaviour
{
    // 데미지 연산을 위한 넘버값 및 데
    //float[] MobData_Number = { HP,ATK,DEF,CoolTime,MoveSpeed};//물론 Stat에서 가져온다
    //float[] PlayerData_Number = { HP, ATK, DEF, CoolTime, MoveSpeed }; // 위와 동일
    //float[] Return_Number;

    //bool[] DoingSpellType = { isFire, isLiquid, isWind, isEarth }; 활성화 된 스펠타입들로 스킬 결정
    
    bool[] test = { true, true, false, true };

    void Start()
    {
        
    }

    
    void Update()
    {
        // 이런 형태 MagicAct(MagicName(DoingSpellType));

    }

    private String MagicName(bool[] SpellTp) // 이걸 아이디로?
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
       //공격은 따로 함수로 만들어서 관리
        if (name == "") { 몬스터 공격}
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
