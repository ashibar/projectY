using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField]
    public GameObject hudDamageText;
    public Transform hudpos;
    private bool isCooltime;
    private void Update()
    {
        if (!isCooltime)
            StartCoroutine(Damagesend(10));
    }

    public IEnumerator Damagesend(int damage)
    {
        isCooltime = true;
        GameObject hudText = Instantiate(hudDamageText);//텍스트
        hudText.transform.position = hudpos.position;//텍스트 포지션
        hudText.GetComponent<DamageText>().damage = damage;
        Debug.Log(damage);
        yield return new WaitForSeconds(3);
        isCooltime = false;
    }
}
