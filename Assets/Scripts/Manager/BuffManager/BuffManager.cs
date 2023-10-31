using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuffManager : MonoBehaviour
{
    [SerializeField] public Unit unit;
    [SerializeField] public Buff_SO buff_SO;
    [SerializeField] public Buff buff;
    [SerializeField] public List<Buff> buffs = new List<Buff>();
    
    public void Awake()
    {
        unit = GetComponentInParent<Unit>();
        buffs.AddRange(GetComponentsInChildren<Buff>());        
    }

    public void Start()
    {
        foreach (Buff b in buffs)
            b.Init(unit.stat, this);
    }

    public void Update()
    {
        Update_Stat();
        //Debug.Log("hp: " + unit.stat_processed.Hp.ToString() + ", armor: " + unit.stat_processed.Armor);
    }

    public void Update_Stat()
    {
        Stat gstat = new Stat(unit.stat);
        Stat pstat = new Stat(unit.stat);
        if (buffs.Count > 0)
        {
            foreach (Buff b in buffs)
            {
                if (b == null) continue;
                b.BuffStat(pstat);
            }
        }
        unit.stat_processed = pstat;
    }

    public void BuffEndListener(Buff buff)
    {
        buffs.Remove(buff);
    }

    public GameObject AddBuff(GameObject buffobj)
    {
        GameObject clone = Instantiate(buffobj, transform);
        buffs.Add(buffobj.GetComponent<Buff>());

        return clone;
    }

    //public void Buffcheck(Stat gstat, Stat pstat, Buff buff)
    //{
    //    switch (buff.Buff_Type)
    //    {
    //        case Buff_SO.BuffType.Buff_Hp:
    //            pstat.Hp = Buffchanger(gstat.Hp, buff.Buff_value);
    //            break;

    //        case Buff_SO.BuffType.Buff_Mp:

    //            break;

    //        case Buff_SO.BuffType.Buff_Armmor:
    //            pstat.Armor = Buffchanger(gstat.Armor, buff.Buff_value);
    //            break;

    //        case Buff_SO.BuffType.Buff_Speed:
    //            pstat.Speed = Buffchanger(gstat.Speed, buff.Buff_value);
    //            break;

    //        case Buff_SO.BuffType.Buff_AttackSpeed:

    //            break;

    //        case Buff_SO.BuffType.Buff_AttackDamage:
    //            pstat.Damage = Buffchanger(gstat.Damage, buff.Buff_value);
    //            break;

    //    }
    //}

    //public float Buffchanger(float val_unit, float val_buff)
    //{
    //    return val_unit * val_buff;

    //    //if(buffs.Count > 0)
    //    //{
    //    //    float temp = 0;
    //    //    for (int i = 0; i <buffs.Count; i++)
    //    //    {
    //    //        if (buffs[i].Buff_Type.Equals(type))
    //    //            temp += val_unit * buffs[i].Buff_value;
    //    //    }
    //    //    return val_unit+temp;
    //    //}
    //    //else
    //    //{
    //    //    return val_unit;
    //    //}
    //}

}
