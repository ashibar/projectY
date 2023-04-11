using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuffManager : MonoBehaviour
{
    public Unit unit;
    [SerializeField]
    public Buff_SO buff_SO;
    [SerializeField]
    public Buff buff;
    [SerializeField]
    public List<Buff> buffs = new List<Buff>();
    
    

    
    public void Awake()
    {
        unit = GetComponentInParent<Unit>();
        buff = GetComponentInChildren<Buff>();
        buff = new Buff(buff_SO);

        
    }
    
    public void Update()
    {
        Getstat();
        Setstat();
        
    }
    public void Getstat()
    {
        Stat gstat = unit.stat;
        if (gstat != null)
        {
           
        }
        
        
    }
    public void Setstat()
    {
       Stat sstat = unit.stat_processed;
    }
    public void Buffcheck(Buff_SO.BuffType type)
    {
        switch (type)
        {
            case Buff_SO.BuffType.Buff_Hp:
                unit.stat_processed.Hp = Buffchanger(buff_SO.Buff_Type, unit.stat.Hp);
                break;

            case Buff_SO.BuffType.Buff_Mp:
                
                break;

            case Buff_SO.BuffType.Buff_Armmor:
                unit.stat.Armor=Buffchanger(buff_SO.Buff_Type,unit.stat.Armor);
                break;

            case Buff_SO.BuffType.Buff_Speed:

                break;

            case Buff_SO.BuffType.Buff_AttackSpeed:

                break;

            case Buff_SO.BuffType.Buff_AttackDamage:

                break;
        }
    }
    IEnumerator OnBuffCoroutine(float du, float va, float cur)
    {
        
        

        yield return new WaitForSeconds(du);

    }
    public float Buffchanger(Buff_SO.BuffType type,float val)
    {
        if(buffs.Count > 0)
        {
            float temp = 0;
            for (int i = 0; i <buffs.Count; i++)
            {
                if (buffs[i].Buff_Type.Equals(type))
                    temp += val * buffs[i].Buff_value;
            }
            return val+temp;
        }
        else
        {
            return val;
        }
    }

}
