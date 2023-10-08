using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage_Husk : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color alpha;
    private float alphaspeed = 4;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alpha = spriteRenderer.color;
        StartCoroutine(Destroy_routine());
    }

    private void Update()
    {
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaspeed);
        spriteRenderer.color = alpha;
    }

    private IEnumerator Destroy_routine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
