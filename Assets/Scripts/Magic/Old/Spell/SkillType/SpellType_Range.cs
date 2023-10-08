using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellType_Range : MonoBehaviour
{
    [SerializeField] public GameObject attackPrefab;
    [SerializeField] public float attackDelay = 1f;
    [SerializeField] public float attackDuration = 2f;
    [SerializeField] public float attackRange = 3f;

    private bool isAttacking = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                StartCoroutine(PerformAttack());
            }
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        yield return new WaitForSeconds(attackDelay);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject attack = Instantiate(attackPrefab, mousePosition, Quaternion.identity);
        CircleCollider2D collider = attack.AddComponent<CircleCollider2D>();
        collider.radius = attackRange;

        StartCoroutine(ShrinkAttack(collider, attackDuration));

        yield return new WaitForSeconds(attackDuration);

        Destroy(attack);
        Destroy(collider);

        isAttacking = false;
    }

    private IEnumerator ShrinkAttack(CircleCollider2D collider, float duration)
    {
        float SpellDuration = 0f;
        float startSize = attackRange;

        while (SpellDuration < duration)
        {
            float normalizedTime = SpellDuration / duration;
            float SpellSize = Mathf.Lerp(startSize, 0f, normalizedTime);

            collider.radius = SpellSize;

            SpellDuration += Time.deltaTime;
            yield return null;
        }
    }

}
