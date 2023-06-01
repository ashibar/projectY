using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Parts_RenderDamageText : Parts_OnColide
{
    [SerializeField] private GameObject text_obj;

    protected override void CollisionProcess(Applier_parameter para)
    {
        Damagesend(para, para.Stat.Spell_DMG);
    }

    public void Damagesend(Applier_parameter para, float damage)
    {
        GameObject hudText = Instantiate(text_obj, para.Proj.transform.position, Quaternion.identity, Holder.damageText_holder);//≈ÿΩ∫∆Æ
        hudText.GetComponent<DamageText>().damage = damage;
    }
}
