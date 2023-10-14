using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private Unit owner;
    [SerializeField] private List<Spell_Core> cores = new List<Spell_Core>();
    [SerializeField] private List<Spell_Part> parts = new List<Spell_Part>();
    [SerializeField] private List<Spell_Passive> passives = new List<Spell_Passive>();
    [SerializeField] string target;

    [SerializeField] public Transform passiveholder;

    private void Awake()
    {
        passiveholder = transform.Find("Passives");

        GetSpellCompoenents();
    }

    private void GetSpellCompoenents()
    {
        cores.AddRange(GetComponentsInChildren<Spell_Core>());
        parts.AddRange(GetComponentsInChildren<Spell_Part>());
        passives.AddRange(GetComponentsInChildren<Spell_Passive>());
    }

    private void Start()
    {
        owner = GetComponentInParent<Unit>();
        foreach (Spell_Core core in cores)
        {
            core.SetUnits(owner, target);
        }
    }

    private void Update()
    {
        foreach (Spell_Core core in cores)
        {
            core.SetVector(target, owner.dir_toMove, owner.dir_toShoot, owner.pos_toShoot);
            core.SetStat(Stat_Process());
        }
    }

    private Stat Stat_Process()
    {
        Stat stat_processed = new Stat(owner.stat);
        foreach (Spell_Passive p in passives)
            if (p.additional_stat != null)
                stat_processed += p.additional_stat;

        return stat_processed;
    }

    public void ClearSpell()
    {
        Spell[] spells = GetComponentsInChildren<Spell>();
        foreach (Spell s in spells)
            if (s != null)
                Destroy(s.gameObject);
    }

    public void SetSpell(GameObject temp)
    {
        MoveChildrenToTarget(temp.transform, gameObject.transform);
        GetSpellCompoenents();
        foreach (Spell_Core core in cores)
            core.InitElement();
    }

    public void SetActiveAll(bool value)
    {
        foreach (Spell_Core core in cores)
            core.SetActive(value);
    }

     private void MoveChildrenToTarget(Transform source, Transform target)
    {
        foreach (Transform child in source)
        {
            // 자식 GameObject의 부모를 대상 GameObject로 설정
            child.SetParent(target);
        }
    }
}
