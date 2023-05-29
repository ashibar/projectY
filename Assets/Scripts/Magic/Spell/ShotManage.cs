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
    public Vector2 dir_toMove;
    public Vector2 dir_toShoot;
    public Vector2 pos_toShoot;
    //================================================
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
    //================================================
    [SerializeField] private bool isActiveMagic = true;
    public bool IsActiveMagic { get => isActiveMagic; set => isActiveMagic = value; }
    //================================================
    // parts - 이용욱
    [SerializeField] public Stat_Spell_so stat_Spell_So;
    [SerializeField] public Stat_Spell stat_spell;
    [SerializeField] public List<Parts> parts = new List<Parts>();
    [SerializeField] private List<Action<Applier_parameter>> appliers = new List<Action<Applier_parameter>>();
    [SerializeField] private List<Action<Applier_parameter>> appliers_OnShot = new List<Action<Applier_parameter>>();
    [SerializeField] private List<Action<Applier_parameter>> appliers_OnUpdate = new List<Action<Applier_parameter>>();
    [SerializeField] private List<Action<Applier_parameter>> appliers_OnColide = new List<Action<Applier_parameter>>();
    [SerializeField] private List<Action<Applier_parameter>> appliers_OnInstantiate = new List<Action<Applier_parameter>>();

    protected void Awake()
    {
        stat_spell = new Stat_Spell(stat_Spell_So);
        parts.AddRange(GetComponentsInChildren<Parts>());
    }

    protected void Start() { 
        foreach (Parts p in parts)
        {
            p.Stat_spell = stat_spell;
        }
        SortParts(parts);
        //if(!isChecked || isUseSpell) // 이렇게 안해주면 작동안함!!!!!!
        //{
        //    isChecked = true;
        //    isUseSpell = false;
        //}
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)&&isActiveMagic) {
            Shoot_Temp();
        }

        //if (isSoleSpell) SkillRangeType = "SOLE";
        //if (isBuffSpell) SkillRangeType = "BUFF";
        //if (isMultiSpell) SkillRangeType = "MULTY";
        //{
        //    if (SkillRangeType == "SOLE")
        //    {
        //        if (isUseSpell) StartCoroutine(ResetSkillCoroutine(cooltime));
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            if (isChecked) Shoot();
        //        }
        //    }
        //    else
        //    {
        //        if (isUseSpell) StartCoroutine(ResetSkillCoroutine(cooltime));
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            if (isChecked) RangeShoot();
        //        }
        //    }
        //}
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
            Spell.GetComponent<Rigidbody2D>().velocity = dir_toShoot * Spell_speed;
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

    [SerializeField] private bool isCooltime;
    [SerializeField] private bool isInterrupted;
    // 발사 함수
    // stat_spell에서 쿨타임을 받아 발사 주기 결정
    private async void Shoot_Temp()
    {
       if (!isCooltime)
        {
            isCooltime = true;
            await Shoot_Task(stat_spell.Spell_CoolTime);
            isCooltime = false;
        }
    }

    // Task 반환 발사 내부 함수
    private async Task Shoot_Task(float duration)
    {
        // 투사체 원본을 복제하고, appliers_update/collides를 복제본에 넣음
        // 복제본의 발사 처리기 작동
        GameObject temp;
        for (int i = 0; i < stat_spell.Spell_Multy_EA; i++)
        {
            temp = Instantiate(Spells[0], transform.position, Quaternion.identity);
            temp.GetComponent<SpellProjectile>().appliers_update.AddRange(appliers_OnUpdate);
            temp.GetComponent<SpellProjectile>().appliers_collides.AddRange(appliers_OnColide);
            temp.GetComponent<SpellProjectile>().stat_spell = stat_spell;
            ShotProcess(temp, stat_spell);
        }
        
        // 쿨타임동안 기다리도록 Task를 반환
        float end = Time.time + duration;
        while (Time.time < end)
        {
            if (isInterrupted)
                await Task.FromResult(false);
            await Task.Yield();
        }
    }

    // Parts 정렬 함수
    private void SortParts(List<Parts> parts)
    {
        // appliers 초기화
        appliers_OnShot.Clear();
        appliers_OnUpdate.Clear();
        appliers_OnColide.Clear();
        
        // parts의 appliers종류에 따라 list에 분배
        foreach (Parts p in parts)
        {
            switch (p.Sort)
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
                case Parts.Parts_Sort.OnInstantiate:
                    appliers_OnInstantiate.Add(p.Applier);
                    break;
            }
        }
        
    }

    // 발사 처리기 작동 함수
    private void ShotProcess(GameObject temp, Stat_Spell stat)
    {
        foreach (Action<Applier_parameter> app in appliers_OnShot)
        {
            app(new Applier_parameter(temp, stat, null, dir_toMove, dir_toShoot, pos_toShoot));
        }
    }

    // 원본 투사체 설정
    public void SetSpell(GameObject origin)
    {
        Spells[0] = origin;
    }

    // 파괴시 함수
    // async문을 강제로 빠져나옴
    private void OnDestroy()
    {
        isInterrupted = true;
    }
}
