using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellType_Split : MonoBehaviour
{
    [SerializeField] public GameObject SpellPrefab;
    [SerializeField] public float Delay = 1f;
    [SerializeField] public float Duration = 200f;
    [SerializeField] public float SpellAngle = 60f;
    [SerializeField] public float SpellRange = 2f;
    [SerializeField] private float SpinRotationColider = 5f;

    private bool isAttacked = false;

    private void Start()
    {
        if(SpellPrefab == null)
        {
            SpellPrefab = GetComponent<GameObject>();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!isAttacked)
            {
                StartCoroutine(PerformAttack());
            }
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacked = true;
        Vector2 mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 player_Pos = transform.position;
        Vector2 Direction = (mouse_Pos - player_Pos).normalized;

        yield return new WaitForSeconds(Delay);

        

        Quaternion baseRotation = Quaternion.AngleAxis(35f, Vector3.forward);
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Direction);

        GameObject Spells = Instantiate(SpellPrefab, player_Pos, rotation * baseRotation);
        Destroy(Spells, Duration);

        // Spells ��ü�� ȸ������ �����մϴ�.
        Spells.transform.rotation = rotation * baseRotation;



        //Colider �� ����
        PolygonCollider2D collider = Spells.AddComponent<PolygonCollider2D>();
        collider.points = CreateFanPoints(SpellAngle, SpellRange);

        //Colider�� Spells�� �ڽİ�ü�� �����Ͽ� Spells�� ȸ�������� �����ǵ��� ����
        //�̷��� ���� ������ ������ �̻��ϰ� ȸ����
        
        //���� �ľǵ� ���� : �������� ��ų�� ���ÿ� �����Ǹ� Colider�� ȸ������ 
        //������ ��׷��� �̻��ϰ� ��µ�. �̴� 

        GameObject colliderObject = new GameObject("Collider");
        colliderObject.transform.parent = Spells.transform.parent;
        colliderObject.transform.position = Spells.transform.position;

        //Colider�� ���� ȸ���� + �߰� ������ ȸ�� ���� ����
        Quaternion coliderRotation = Quaternion.AngleAxis(-SpinRotationColider, Vector3.forward);
        colliderObject.transform.rotation = coliderRotation * rotation * baseRotation;

        PolygonCollider2D colliderComponent = colliderObject.AddComponent<PolygonCollider2D>();
        colliderComponent.points = collider.points;


        StartCoroutine(DestroyAttack(Spells, Duration));

        isAttacked = false;
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
