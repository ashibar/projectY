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
    protected float ReduceSpeed = 0.2f; // 줄어드는 속도. 무조건 0~1 사이의 값만 작성해야됨.

    private bool isDeleted = false;
    [SerializeField]
    private bool isRange = false;
    [SerializeField]
    public Stat_Spell stat_spell;
    [SerializeField]
    public List<Action<Applier_parameter>> appliers_update = new List<Action<Applier_parameter>>();
    [SerializeField]
    public List<Action<Applier_parameter>> appliers_collides = new List<Action<Applier_parameter>>();
    protected virtual void Start()
    {
        if (ReduceSpeed <= 0 || ReduceSpeed >= 1) ReduceSpeed = 0.5f;
        AutoDelete(duration);
    }
    private void Update()
    {
        UpdateProcess(stat_spell);
    }
    protected virtual async void AutoDelete(float duration)
    {

        float end = Time.time + duration;

        while (Time.time < end)
        {
            //transform.localScale = new Vector2(transform.localScale.x - 1f * ReduceSpeed / duration * Time.deltaTime,
            //transform.localScale.y - 1f * ReduceSpeed / duration * Time.deltaTime);
            await Task.Yield();
        }
        if (!isDeleted) Destroy(gameObject);
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

    //    if (!isDeleted) Destroy(gameObject);
    //}

    private void UpdateProcess(Stat_Spell stat)
    {
        foreach (Action<Applier_parameter> app in appliers_update)
        {
            app(new Applier_parameter(gameObject, stat));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            foreach (Action<Applier_parameter> app in appliers_collides)
                app(new Applier_parameter(gameObject, stat_spell, collision));

            isDeleted = true;
            Destroy(gameObject);
        }

    }
}