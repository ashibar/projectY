using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
/// <summary>
/// �ۼ��� : ������
/// ������ ������Ʈ �ñ� : 23/3/24 / 19:30
/// ������Ʈ ���� : ��Ÿ�� ����
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
    //�̷��� ������ ��ũ��Ʈ�� �Ѱ� �� ������ �����´�.
    //�Ʒ��ʿ� �����ϴ� ��� �����ʹ� �װ����� ����
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
    // parts - �̿��
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
        //if(!isChecked || isUseSpell) // �̷��� �����ָ� �۵�����!!!!!!
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

    protected int DoingSpell() //���° �������� �����ش�.
    {
        int SpellNumbers = 0; // ���� �̻��� ���� �־ 0���� �⺻�� ����
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
        //��������� ���� ��

        //Vector2 len = (Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //GameObject Spell = Instantiate(Spells[DoingSpell()], len, Quaternion.identity);
        //Spell.GetComponent<Rigidbody2D>();
        //��������� RangeShoot

        
    }
    public virtual void RangeShoot()
    {
        isUseSpell = true;
        isChecked = false;
        Vector2 len = (Camera.main.ScreenToWorldPoint(Input.mousePosition));

        GameObject Spell = Instantiate(Spells[DoingSpell()], len, Quaternion.identity);
        Spell.GetComponent<Rigidbody2D>();


    }

    IEnumerator ResetSkillCoroutine(float coltimes) //��ų ��Ÿ�� 
    {
        const float baseTime = 0.1f; // BaseTime�� �ּҴ���
        isUseSpell = false; //���ڸ��� �ڱ� �ڽ��� ������ ��Ȱ��ȭ // �ȱ׷��� Update���� �����ϰ� �����
        while (coltimes > 0) //��Ÿ�� ���� ����, coltimes�� �޾Ƽ� baseTime�ʸ�ŭ�� ����
        {
            coltimes -= baseTime;
            yield return new WaitForSeconds(baseTime);
        }
        isChecked = true; //Shoot Ȱ��ȭ ����
        
        yield break;
    }

    // ������� ���� ������ Shoot()�Լ� - �̿��

    [SerializeField] private bool isCooltime;
    [SerializeField] private bool isInterrupted;
    // �߻� �Լ�
    // stat_spell���� ��Ÿ���� �޾� �߻� �ֱ� ����
    private async void Shoot_Temp()
    {
       if (!isCooltime)
        {
            isCooltime = true;
            await Shoot_Task(stat_spell.Spell_CoolTime);
            isCooltime = false;
        }
    }

    // Task ��ȯ �߻� ���� �Լ�
    private async Task Shoot_Task(float duration)
    {
        // ����ü ������ �����ϰ�, appliers_update/collides�� �������� ����
        // �������� �߻� ó���� �۵�
        GameObject temp;
        for (int i = 0; i < stat_spell.Spell_Multy_EA; i++)
        {
            temp = Instantiate(Spells[0], transform.position, Quaternion.identity);
            temp.GetComponent<SpellProjectile>().appliers_update.AddRange(appliers_OnUpdate);
            temp.GetComponent<SpellProjectile>().appliers_collides.AddRange(appliers_OnColide);
            temp.GetComponent<SpellProjectile>().stat_spell = stat_spell;
            ShotProcess(temp, stat_spell);
        }
        
        // ��Ÿ�ӵ��� ��ٸ����� Task�� ��ȯ
        float end = Time.time + duration;
        while (Time.time < end)
        {
            if (isInterrupted)
                await Task.FromResult(false);
            await Task.Yield();
        }
    }

    // Parts ���� �Լ�
    private void SortParts(List<Parts> parts)
    {
        // appliers �ʱ�ȭ
        appliers_OnShot.Clear();
        appliers_OnUpdate.Clear();
        appliers_OnColide.Clear();
        
        // parts�� appliers������ ���� list�� �й�
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

    // �߻� ó���� �۵� �Լ�
    private void ShotProcess(GameObject temp, Stat_Spell stat)
    {
        foreach (Action<Applier_parameter> app in appliers_OnShot)
        {
            app(new Applier_parameter(temp, stat, null, dir_toMove, dir_toShoot, pos_toShoot));
        }
    }

    // ���� ����ü ����
    public void SetSpell(GameObject origin)
    {
        Spells[0] = origin;
    }

    // �ı��� �Լ�
    // async���� ������ ��������
    private void OnDestroy()
    {
        isInterrupted = true;
    }
}
