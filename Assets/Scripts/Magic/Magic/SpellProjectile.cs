using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
/***
 * �ۼ��� : ������
 * ������ : 23-4-6
 * ���� ���� : AutoReduce�Լ� �߰�// RangeSpell�� ��� ReduceSpeed�� ����ؼ� ũ�� ����
 */
public class SpellProjectile : MonoBehaviour
{
    [SerializeField]
    private float duration;
    [SerializeField]
    protected float ReduceSpeed = 0.2f; // �پ��� �ӵ�. ������ 0~1 ������ ���� �ۼ��ؾߵ�.

    private bool isDeleted = false;
    [SerializeField]
    private bool isRange = false;
    protected virtual void Awake()
    {
        if (ReduceSpeed <= 0 || ReduceSpeed >= 1) ReduceSpeed = 0.5f;
    }
    protected virtual void Start()
    {
        AutoDelete(duration);
    }
    private void Update()
    {
        
    }
    protected virtual async void AutoDelete(float duration)
    {

        float end = Time.time + duration;

        while(Time.time < end)
        {
            //transform.localScale = new Vector2(transform.localScale.x - 1f * ReduceSpeed / duration * Time.deltaTime,
            //transform.localScale.y - 1f * ReduceSpeed / duration * Time.deltaTime);
            await Task.Yield();
        }
        if(!isDeleted) Destroy(gameObject);
    }
    private async void AutoReduce(float duration)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            transform.localScale = new Vector2(transform.localScale.x - 1f * ReduceSpeed / duration * Time.deltaTime,
                transform.localScale.y - 1f * ReduceSpeed / duration * Time.deltaTime);
            await Task.Yield();
        }
        if (!isDeleted) Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //collision.GetComponent<Enemy>().Delete_FromCloneList();
            //Destroy(collision.gameObject);
            // Destory => Enemy ���� ����
            // ������ ������ SpellStat�� �ִ� ������ ������ ������ ���⼭.

            isDeleted = true;
            Destroy(gameObject);
        }

    }
}
