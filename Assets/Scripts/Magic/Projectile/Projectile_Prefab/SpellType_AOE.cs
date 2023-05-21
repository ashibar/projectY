using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellType_AOE : MonoBehaviour
{
    [SerializeField] public float Circleradius = 3f;
    [SerializeField] SpellProjectile proj;

    public float radius = 2.0f; // Circle�� ������ (Test��)
    public float spellTimes; // SpellProjectile�� Duration�� ��ġ��Ű������

    private Vector3 originalScale; 
    
    
    private void Start()
    {
        proj = GetComponent<SpellProjectile>();
        spellTimes = proj.Duration;
        originalScale = transform.localScale;
        Debug.Log(spellTimes);
        Debug.Log(proj.Duration);
        StartCoroutine(ShrinkCircle());
    }

   
    // SpellTime ���� ������ �ӵ��� �پ��� �Լ�
    private IEnumerator ShrinkCircle()
    {
        float elapsedTime = 0.0f;
        
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
