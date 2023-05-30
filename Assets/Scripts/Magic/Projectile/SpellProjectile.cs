using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
/***
 * ÀÛ¼ºÀÚ : ¹ÚÁ¾¼º
 * ¼öÁ¤ÀÏ : 23-4-6
 * ¼öÁ¤ ³»¿ë : AutoReduceÇÔ¼ö Ãß°¡// RangeSpellÀÎ °æ¿ì ReduceSpeed¿¡ ºñ·ÊÇØ¼­ Å©±â °¨¼Ò
 */
public class SpellProjectile : MonoBehaviour
{
    
    [SerializeField]
    private float duration;
    [SerializeField]
    protected float ReduceSpeed = 0.2f; // ÁÙ¾îµå´Â ¼Óµµ. ¹«Á¶°Ç 0~1 »çÀÌÀÇ °ª¸¸ ÀÛ¼ºÇØ¾ßµÊ.
    [SerializeField] public Applier_parameter para;

    private bool isDeleted = false;
    
    [SerializeField]
    public Stat_Spell stat_spell;
    [SerializeField]
    public List<Action<Applier_parameter>> appliers_update = new List<Action<Applier_parameter>>();
    [SerializeField]
    public List<Action<Applier_parameter>> appliers_collides = new List<Action<Applier_parameter>>();


    public float Duration { get => stat_spell.Spell_Duration; }

    protected virtual void Start()
    {
        duration = stat_spell.Spell_Duration;
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
            app(para);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //collision.GetComponent<Enemy>().Delete_FromCloneList();
            //Destroy(collision.gameObject);
            // Destory => Enemy ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
            // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ SpellStatï¿½ï¿½ ï¿½Ö´ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½â¼­.

            // ï¿½æµ¹ï¿½ï¿½ ï¿½Ûµï¿½ï¿½ï¿½ applier - ï¿½Ì¿ï¿½ï¿?
            Applier_parameter para_col = new Applier_parameter(para);
            para_col.Collision = collision;
            foreach (Action<Applier_parameter> app in appliers_collides)
                app(para_col);



            isDeleted = true;
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        isDeleted = true;
    }
}