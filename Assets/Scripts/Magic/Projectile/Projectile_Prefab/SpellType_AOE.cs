using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellType_AOE : MonoBehaviour
{
    [SerializeField] public float Circleradius = 3f;
    [SerializeField] Stat_Spell stat;

    public float radius = 2.0f; // Circle�� ������ (Test��)
    public float spellTimes; // SpellProjectile�� Duration�� ��ġ��Ű������

    private Vector3 originalScale; 
    
    
    private void Start()
    {
        spellTimes = stat.Spell_Duration;
        originalScale = transform.localScale;
        
        StartCoroutine(ShrinkCircle());
    }

   
    // SpellTime ���� ������ �ӵ��� �پ��� �Լ�
    private IEnumerator ShrinkCircle()
    {
        float elapsedTime = 0.0f;
        Debug.Log(spellTimes);
        while (elapsedTime < spellTimes)
        {
            float t = elapsedTime / spellTimes; 
            
            float targetScale = originalScale.magnitude * radius * Mathf.Lerp(1.0f, 0.75f, t); 
            transform.localScale = Vector3.one * targetScale; 
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
    }


}
