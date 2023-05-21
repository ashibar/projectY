using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellType_Splits : MonoBehaviour
{
    [SerializeField] public GameObject SpellPrefab;
    [SerializeField] public float Delay = 1f;
    [SerializeField] public float Duration = 200f;
    [SerializeField] public float SpellAngle = 90f;
    [SerializeField] public float SpellRange = 2f;
    [SerializeField] public Vector2 mouse_dir = new(0, 0);
    [SerializeField] SpellProjectile proj;

    private void Start()
    {
        proj = GetComponent<SpellProjectile>();
        Duration = proj.Duration;
        PerformAttack();
    }
    
    private void PerformAttack()
    {
        //Vector2 mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 player_Pos = transform.position;
        Vector2 Direction = (mouse_dir - player_Pos).normalized;


        //Quaternion rotation = Quaternion.LookRotation(Vector3.forward, player_Pos);
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, mouse_dir);
        //이 위의 부분은 그냥 나중에 값 받아오면 됨

       // GameObject Spells = Instantiate(SpellPrefab, player_Pos, rotation);


        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        collider.points = CreateFanPoints(SpellAngle, SpellRange);

        

    }

    private IEnumerator DestroyAttack(GameObject attack, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(attack);
        
    }

    private Vector2[] CreateFanPoints(float angle, float radius)
    {
        int segments = Mathf.RoundToInt(angle / 10f);
        float anglePerSegment = angle / segments;
        int pointCount = segments + 2;

        Vector2[] points = new Vector2[pointCount];

        points[0] = Vector2.zero;

        for (int i = 0; i <= segments; i++)
        {
            float rad = Mathf.Deg2Rad * (anglePerSegment * i);
            points[i + 1] = new Vector2(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius);
        }

        return points;
    }

}
