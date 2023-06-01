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
        GameObject hudText = Instantiate(hudDamageText);//�ؽ�Ʈ
        hudText.transform.position = hudpos.position;//�ؽ�Ʈ ������
        hudText.GetComponent<DamageText>().damage = damage;
        Debug.Log(damage);
        
    }
}
