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
            //collision.GetComponent<Enemy>().Delete_FromCloneList();
            //Destroy(collision.gameObject);
            // Destory => Enemy ���� ����
            // ������ ������ SpellStat�� �ִ� ������ ������ ������ ���⼭.
            
            // �浹�� �۵��� applier - �̿��
            foreach (Action<GameObject, Stat_Spell, Collider2D> app in appliers_collides)
                app(gameObject, stat_spell, collision);
            
            

            isDeleted = true;
            Destroy(gameObject);
        }

    }
}
