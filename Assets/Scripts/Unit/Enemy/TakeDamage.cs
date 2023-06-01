using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField]
    public GameObject hudDamageText;
    public Transform hudpos;
    private void Update()
    {
        Damagesend(10);
    }

    public void Damagesend(int damage)
    {
        GameObject hudText = Instantiate(hudDamageText);//텍스트
        hudText.transform.position = hudpos.position;//텍스트 포지션
        hudText.GetComponent<DamageText>().damage = damage;
        Debug.Log(damage);
        
    }
}
