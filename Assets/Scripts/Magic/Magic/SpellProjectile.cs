using System;
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
    private float ReduceSpeed = 0.2f; // 줄어드는 속도. 무조건 0~1 사이의 값만 작성해야됨.
    private bool isDeleted = false;
    [SerializeField]
    private bool isRange = false;
    [SerializeField]
    public Stat_Spell stat_spell;
    [SerializeField]
    public List<Action<GameObject, Stat_Spell, Collider2D>> appliers_update = new List<Action<GameObject, Stat_Spell, Collider2D>>();
    [SerializeField]
    public List<Action<GameObject, Stat_Spell, Collider2D>> appliers_collides = new List<Action<GameObject, Stat_Spell, Collider2D>>();
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

    private void UpdateProcess(Stat_Spell stat)
    {
        foreach (Action<GameObject, Stat_Spell, Collider2D> app in appliers_update)
        {
            app(gameObject, stat, null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Delete_FromCloneList();
            Destroy(collision.gameObject);
            // Destory => Enemy 에서 관리
            // 데미지 연산은 SpellStat에 있는 값으로 데미지 연산은 여기서.
            
            // 충돌시 작동될 applier - 이용욱
            foreach (Action<GameObject, Stat_Spell, Collider2D> app in appliers_collides)
                app(gameObject, stat_spell, collision);
            
            

            //이곳에 AutoReduce작성
            isDeleted = true;
            Destroy(gameObject);
        }

    }
}
