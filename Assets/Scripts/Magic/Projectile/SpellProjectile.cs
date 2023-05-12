using System;
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
            if (isDeleted)
                await Task.FromResult(false);
            await Task.Yield();
        }
        if (!isDeleted) Destroy(gameObject);
    }
    private async void AutoReduce(float duration)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            if (isDeleted)
                await Task.FromResult(false);
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
            //collision.GetComponent<Enemy>().Delete_FromCloneList();
            //Destroy(collision.gameObject);
            // Destory => Enemy ���� ����
            // ������ ������ SpellStat�� �ִ� ������ ������ ������ ���⼭.

            // �浹�� �۵��� applier - �̿��?
            foreach (Action<Applier_parameter> app in appliers_collides)
                app(new Applier_parameter(gameObject, stat_spell, collision));



            isDeleted = true;
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        isDeleted = true;
    }
}