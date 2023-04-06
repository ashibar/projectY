using System;
using System.Collections;
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
    [SerializeField] protected bool isRangeSpell = false;
    //================================================
    [SerializeField] protected String SkillRangeType = "SOLE";



    protected void Start() { 
        if(!isChecked || isUseSpell) // 이렇게 안해주면 작동안함!!!!!!
        {
            isChecked = true;
            isUseSpell = false;
        }
    }

    public void Update()
    {
        if (isSoleSpell) SkillRangeType = "SOLE";
        if (isBuffSpell) SkillRangeType = "BUFF";
        if (isMultiSpell) SkillRangeType = "MULTI";
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
            
            GameObject Spell = Instantiate(Spells[DoingSpell()], transform.position, Quaternion.identity);
            
            Spell.GetComponent<Rigidbody2D>().velocity = dir_toMouse * Spell_speed;  //후에 추가 예정
            
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
    IEnumerator ReduceRangeCoroutine(float ReduceTime) //스킬 쿨타임 
    {
        Debug.Log("Worked");
        const float baseTime = 0.1f; // BaseTime이 최소단위
        
        isRangeSpell = false; //들어가자마자 자기 자신의 조건을 비활성화 // 안그러면 Update에서 무한하게 실행됨
        while (ReduceTime > 0) //쿨타임 메인 로직, coltimes를 받아서 baseTime초만큼씩 줄임
        {
            ReduceTime -= baseTime;
            
            yield return new WaitForSeconds(baseTime);
        }
        isChecked = true; //Shoot 활성화 로직

        yield break;
    }
}
