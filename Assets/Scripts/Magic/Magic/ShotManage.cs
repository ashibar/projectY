using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// �ۼ��� : ������
/// ������ ������Ʈ �ñ� : 23/3/24 / 19:30
/// ������Ʈ ���� : ��Ÿ�� ����
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
        if(!isChecked || isUseSpell) // �̷��� �����ָ� �۵�����!!!!!!
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
    
    protected int DoingSpell() //���° �������� �����ش�.
    {
        int SpellNumbers = 0; // ���� �̻��� ���� �־ 0���� �⺻�� ����
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

    
    IEnumerator ResetSkillCoroutine(float coltimes) //��ų ��Ÿ�� 
    {
        const float baseTime = 1f; // BaseTime�� �ּҴ���
        isUseSpell = false; //���ڸ��� �ڱ� �ڽ��� ������ ��Ȱ��ȭ // �ȱ׷��� Update���� �����ϰ� �����
        while (coltimes > 0) //��Ÿ�� ���� ����, coltimes�� �޾Ƽ� baseTime�ʸ�ŭ�� ����
        {
            coltimes -= baseTime;
            yield return new WaitForSeconds(baseTime);
        }
        isChecked = true; //Shoot Ȱ��ȭ ����
        
        yield break;
    }



}
