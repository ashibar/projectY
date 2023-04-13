using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
/***
 * 작성자 : 박종성
 * 수정일 : 23-4-6
 * 수정 내용 : AutoReduce함수 추가// RangeSpell인 경우 ReduceSpeed에 비례해서 크기 감소
 */
public class SpellProjectile : MonoBehaviour
{
    [SerializeField]
    private float duration;
    [SerializeField]
    protected float ReduceSpeed = 0.2f; // 줄어드는 속도. 무조건 0~1 사이의 값만 작성해야됨.

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
            // Destory => Enemy 에서 관리
            // 데미지 연산은 SpellStat에 있는 값으로 데미지 연산은 여기서.

            isDeleted = true;
            Destroy(gameObject);
        }

    }
}
