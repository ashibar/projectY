using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculation : MonoBehaviour
{
    [SerializeField] private DamageTextRenderModule damageTextRenderModule;

    private void Awake()
    {
        damageTextRenderModule = GetComponentInChildren<DamageTextRenderModule>();
    }

    public float Calculate(Unit owner, Unit target, Stat stat_processed, Stat_Spell stat_Spell, Color text_color)
    {
        Stat owner_stat = stat_processed;
        Stat target_stat = target.stat_processed;

        float damage = owner_stat.Damage * stat_Spell.Spell_DMG - target_stat.Armor;

        damageTextRenderModule.Damagesend(target.gameObject, (int)damage, text_color);

        return damage >= 0 ? damage : 0;
    }

    public float Calculate(GameObject target, float damage, Color text_color)
    {
        damageTextRenderModule.Damagesend(target.gameObject, (int)damage, text_color);
        return damage >= 0 ? damage : 0;
    }
}
