using System;
using System.Collections;
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
    [SerializeField] protected float cooltime = 1;
    [SerializeField] float Spell_speed = 5f;
    //================================================
    [SerializeField] protected bool isFireSpell = true;
    [SerializeField] protected bool isIceSpell = false;
    [SerializeField] protected bool isEarthSpell = false;
    //================================================

    [SerializeField] protected bool isChecked = true;
    [SerializeField] protected bool isUseSpell = false;
    //================================================
    
    

    
    protected void Start() { 
        if(!isChecked || isUseSpell) // 이렇게 안해주면 작동안함!!!!!!
        {
            isChecked = true;
            isUseSpell = false;
        }
    }
    
    public void Update()
    {
        if (isUseSpell)
        {
            StartCoroutine(ResetSkillCoroutine(cooltime));
        }


        if (Input.GetMouseButtonDown(0))
        {
            
            if(isChecked)
            {
                Shoot();
                isUseSpell = true;
                isChecked = false;
            }
                
            
        }

    }
    
    protected int DoingSpell() //몇번째 스펠인지 정해준다.
    {
        int SpellNumbers = 0; // 만일 이상한 값이 있어도 0으로 기본값 설정
        if (isFireSpell) SpellNumbers = 0;
        if (isIceSpell) SpellNumbers = 1;
        if (isEarthSpell) SpellNumbers = 2;
        return SpellNumbers;
    }
    
   
    public virtual void Shoot()
    {
        {
            Vector2 len = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            Vector2 dir = new Vector2(len.x, len.y).normalized;
            
            GameObject Spell = Instantiate(Spells[DoingSpell()], transform.position, Quaternion.identity);
            
            Debug.Log(dir_toMouse);
            Spell.GetComponent<Rigidbody2D>().velocity = dir * Spell_speed; //dir_toMouse
            
        }
    }

    
    IEnumerator ResetSkillCoroutine(float coltimes) //스킬 쿨타임 
    {
        const float baseTime = 1f; // BaseTime이 최소단위
        isUseSpell = false; //들어가자마자 자기 자신의 조건을 비활성화 // 안그러면 Update에서 무한하게 실행됨
        while (coltimes > 0) //쿨타임 메인 로직, coltimes를 받아서 baseTime초만큼씩 줄임
        {
            coltimes -= baseTime;
            yield return new WaitForSeconds(baseTime);
        }
        isChecked = true; //Shoot 활성화 로직
        
        yield break;
    }



}
