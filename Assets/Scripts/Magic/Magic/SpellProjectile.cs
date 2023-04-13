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

    private bool isDeleted = false;
    [SerializeField]
    private bool isRange = false;
    [SerializeField]
    public Stat_Spell stat_spell;
    [SerializeField]
    public List<Action<GameObject, Stat_Spell, Collider2D>> appliers_update = new List<Action<GameObject, Stat_Spell, Collider2D>>();
    [SerializeField]
    public List<Action<GameObject, Stat_Spell, Collider2D>> appliers_collides = new List<Action<GameObject, Stat_Spell, Collider2D>>();
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
            // Destory => Enemy ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
            // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ SpellStatï¿½ï¿½ ï¿½Ö´ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½â¼­.

            // ï¿½æµ¹ï¿½ï¿½ ï¿½Ûµï¿½ï¿½ï¿½ applier - ï¿½Ì¿ï¿½ï¿?
            foreach (Action<GameObject, Stat_Spell, Collider2D> app in appliers_collides)
                app(gameObject, stat_spell, collision);



            isDeleted = true;
            Destroy(gameObject);
        }

    }
}