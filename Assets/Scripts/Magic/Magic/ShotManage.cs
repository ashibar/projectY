using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
/// <summary>
/// 작성자 : 박종성
/// 마지막 업데이트 시기 : 23/3/24 / 19:30
/// 업데이트 내용 : 쿨타임 적용
/// GameObject = ShotManage // Player
/// </summary>

public class ShotManage : MonoBehaviour
{
    enum MAGICTYPE{SOLE,MULTY,RANGE };
    //public GameObject[] preFabSpell;
    public Vector2 dir_toMouse;
    //================================================
    [SerializeField] private Spell_Stat_so stat_so;
    [SerializeField] private Spell_Stat stat;
    [SerializeField] private GameObject[] Spells;
    //================================================
    //public GameObject[] AllMagicData;
    //이러한 형태의 스크립트를 한개 더 생성해 가져온다.
    //아래쪽에 존재하는 모든 데이터는 그곳에서 관리
    //
    //================================================
    [SerializeField] protected float cooltime = 1;
    [SerializeField] float Spell_speed = 5f;
    [SerializeField] protected float ActiveRangeTime = 1f;

    [SerializeField] protected bool isSoleSpell = true;
    [SerializeField] protected bool isBuffSpell = false;
    [SerializeField] protected bool isMultiSpell = false;
    //================================================

    [SerializeField] protected bool isChecked = true;
    [SerializeField] protected bool isUseSpell = false;
    //================================================
    [SerializeField] protected String SkillRangeType = "SOLE";

    // parts - 이용욱
    [SerializeField] public Stat_Spell stat_spell;
    [SerializeField] public List<Parts> parts = new List<Parts>();
    [SerializeField] private List<Action<GameObject, Stat_Spell, Collider2D>> appliers = new List<Action<GameObject, Stat_Spell, Collider2D>>();
    [SerializeField] private List<Action<GameObject, Stat_Spell, Collider2D>> appliers_OnShot = new List<Action<GameObject, Stat_Spell, Collider2D>>();
    [SerializeField] private List<Action<GameObject, Stat_Spell, Collider2D>> appliers_OnUpdate = new List<Action<GameObject, Stat_Spell, Collider2D>>();
    [SerializeField] private List<Action<GameObject, Stat_Spell, Collider2D>> appliers_OnColide = new List<Action<GameObject, Stat_Spell, Collider2D>>();
    
    
    protected void Start() { 
        if(!isChecked || isUseSpell) // 이렇게 안해주면 작동안함!!!!!!
        {
            isChecked = true;
            isUseSpell = false;
        }
        SortParts(parts);
    }

    public void Update()
    {
        if (isSoleSpell) SkillRangeType = "SOLE";
        if (isBuffSpell) SkillRangeType = "BUFF";
        if (isMultiSpell) SkillRangeType = "MULTY";
        {
            if (SkillRangeType == "SOLE")
            {
                if (isUseSpell) StartCoroutine(ResetSkillCoroutine(cooltime));
                if (Input.GetMouseButtonDown(0))
                {
                    if (isChecked) Shoot();
                }
            }
            else
            {
                if (isUseSpell) StartCoroutine(ResetSkillCoroutine(cooltime));
                if (Input.GetMouseButtonDown(0))
                {
                    if (isChecked) RangeShoot();
                }
            }
        }
    }
    protected int DoingSpell() //몇번째 스펠인지 정해준다.
    {
        int SpellNumbers = 0; // 만일 이상한 값이 있어도 0으로 기본값 설정
        if (isSoleSpell) SpellNumbers = 0;
        if (isBuffSpell) SpellNumbers = 1;
        if (isMultiSpell) SpellNumbers = 2;
        
        return SpellNumbers;
    }
    
   
    public virtual void Shoot()
    {
            isUseSpell = true;
            isChecked = false;
            ////
            GameObject Spell = Instantiate(Spells[DoingSpell()], transform.position, Quaternion.identity);
            Spell.GetComponent<Rigidbody2D>().velocity = dir_toMouse * Spell_speed;
        //여기까지는 기존 샷

        //Vector2 len = (Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //GameObject Spell = Instantiate(Spells[DoingSpell()], len, Quaternion.identity);
        //Spell.GetComponent<Rigidbody2D>();
        //여기까지가 RangeShoot

        
    }
    public virtual void RangeShoot()
    {
        isUseSpell = true;
        isChecked = false;
        Vector2 len = (Camera.main.ScreenToWorldPoint(Input.mousePosition));

        GameObject Spell = Instantiate(Spells[DoingSpell()], len, Quaternion.identity);
        Spell.GetComponent<Rigidbody2D>();


    }

    IEnumerator ResetSkillCoroutine(float coltimes) //스킬 쿨타임 
    {
        const float baseTime = 0.1f; // BaseTime이 최소단위
        isUseSpell = false; //들어가자마자 자기 자신의 조건을 비활성화 // 안그러면 Update에서 무한하게 실행됨
        while (coltimes > 0) //쿨타임 메인 로직, coltimes를 받아서 baseTime초만큼씩 줄임
        {
            coltimes -= baseTime;
            yield return new WaitForSeconds(baseTime);
        }
        isChecked = true; //Shoot 활성화 로직
        
        yield break;
    }

    // 여기부터 새로 구현한 Shoot()함수 - 이용욱

    private bool isCooltime;

    private async void Shoot_Temp()
    {
       if (!isCooltime)
        {
            isCooltime = true;
            await Shoot_Task(cooltime);
            isCooltime = false;
        }
    }

    private async Task Shoot_Task(float duration)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            await Task.Yield();
        }
        GameObject temp;
        ShotProcess(stat_spell);
        for (int i = 0; i < 1/*stat_spell.투사체 갯수*/; i++)
        {
            temp = Instantiate(Spells[DoingSpell()], transform.position, Quaternion.identity);
            temp.GetComponent<SpellProjectile>().appliers_update.AddRange(appliers_OnUpdate);
            temp.GetComponent<SpellProjectile>().appliers_collides.AddRange(appliers_OnColide);
        }
    }

    private void SortParts(List<Parts> parts)
    {
        appliers_OnShot.Clear();
        appliers_OnUpdate.Clear();
        appliers_OnColide.Clear();
        foreach (Parts p in parts)
        {
            switch (p.sort)
            {
                case Parts.Parts_Sort.OnShot:
                    appliers_OnShot.Add(p.Applier);
                    break;
                case Parts.Parts_Sort.OnUpdate:
                    appliers_OnUpdate.Add(p.Applier);
                    break;
                case Parts.Parts_Sort.OnColide:
                    appliers_OnColide.Add(p.Applier);
                    break;
            }
        }
        
    }

    private void ShotProcess(Stat_Spell stat)
    {
        foreach (Action<GameObject, Stat_Spell, Collider2D> app in appliers_OnShot)
        {
            app(null, stat, null);
        }
    }
}
