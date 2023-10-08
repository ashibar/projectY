using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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

    public void SetSpell(List<GameObject> prefabs)
    {
        foreach (GameObject prefab in prefabs) SetCoreNPassive(prefab);
        cores.AddRange(GetComponentsInChildren<Spell_Core>());
        passives.AddRange(GetComponentsInChildren<Spell_Passive>());
        foreach (GameObject prefab in prefabs) SetPartNElement(prefab);
        parts.AddRange(GetComponentsInChildren<Spell_Part>());
    }

    public void SetCoreNPassive(GameObject prefab)
    {
        passiveholder = transform.Find("Passives");

        char sort = prefab.GetComponent<Spell>().GetCode()[0];
        switch (sort)
        {
            case 'a': Instantiate(prefab, transform); break;
            case 'd': Instantiate(prefab, passiveholder); break;
            default: break;
        }
    }

    public void SetPartNElement(GameObject prefab)
    {
        char sort = prefab.GetComponent<Spell>().GetCode()[0];
        switch (sort)
        {
            case 'b':
                foreach (Spell_Core core in cores)
                    if (string.Equals(core.GetCode(), prefab.GetComponent<Spell>().parent_code))
                        Instantiate(prefab, core.transform);
                break;
            case 'c':
                foreach (Spell_Core core in cores)
                    if (string.Equals(core.GetCode(), prefab.GetComponent<Spell>().parent_code))
                        Instantiate(prefab, core.transform);
                break;
            default: break;
        }
    }
}
