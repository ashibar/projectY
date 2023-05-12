using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellType_AOE : MonoBehaviour
{
    [SerializeField] public float Circleradius = 3f;

    public float radius = 2.0f; // Circle의 반지름 (Test용)
    public float spellTimes = 5.0f; // SpellProjectile의 Duration과 일치시키여야함

    private Vector3 originalScale; 
    
    private void Start()
    {
        originalScale = transform.localScale;
        
        StartCoroutine(ShrinkCircle());
    }

   
    // SpellTime 동안 일정한 속도로 줄어드는 함수
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
