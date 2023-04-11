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
    private float ReduceSpeed = 0.2f; // �پ��� �ӵ�. ������ 0~1 ������ ���� �ۼ��ؾߵ�.
    private bool isDeleted = false;
    [SerializeField]
    private bool isRange = false;
    private void Start()
    {
        if (ReduceSpeed <= 0 || ReduceSpeed >= 1) ReduceSpeed = 0.5f;
        //if (isRange) AutoReduce(duration);
        //else 
            AutoDelete(duration);
    }
    private void Update()
    {
        
    }
    private async void AutoDelete(float duration)
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
    //private async void AutoReduce(float duration)
    //{
    //    float end = Time.time + duration;
    //    while (Time.time < end)
    //    {
    //        transform.localScale = new Vector2(transform.localScale.x - 1f * ReduceSpeed / duration * Time.deltaTime,
    //            transform.localScale.y - 1f * ReduceSpeed / duration * Time.deltaTime);
    //        await Task.Yield();
    //    }
    //    Debug.Log(transform.localScale.x);

    //    if (!isDeleted) Destroy(gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Delete_FromCloneList();
            Destroy(collision.gameObject);
            // Destory => Enemy ���� ����
            // ������ ������ SpellStat�� �ִ� ������ ������ ������ ���⼭.

            //�̰��� AutoReduce�ۼ�
            isDeleted = true;
            Destroy(gameObject);
        }

    }
}
