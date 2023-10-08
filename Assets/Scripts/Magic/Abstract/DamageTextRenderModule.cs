using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextRenderModule : MonoBehaviour
{
    [SerializeField] private GameObject text_obj;

    public void Damagesend(GameObject projectile, float damage)
    {
        GameObject hudText = Instantiate(text_obj, projectile.transform.position, Quaternion.identity, Holder.damageText_holder);//≈ÿΩ∫∆Æ
        hudText.GetComponent<DamageText>().damage = damage;
    }
}
