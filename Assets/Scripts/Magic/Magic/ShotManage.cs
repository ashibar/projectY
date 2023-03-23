using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class ShotManage : MonoBehaviour
{
    //public GameObject[] preFabSpell;
    Camera cam;
    [SerializeField]
    private Spell_Stat_so stat_so;
    [SerializeField]
    private Spell_Stat stat;

    public Vector2 dir_toMouse;

    [SerializeField]
    protected bool iscooltime = false;
    //================================================
    [SerializeField]
    protected bool isFireSpell = true;
    [SerializeField]
    protected bool isIceSpell = false;
    [SerializeField]
    protected bool isEarthSpell = false;
    [SerializeField]
    protected bool isSpell = false;
    //================================================
    [SerializeField]
    protected bool isMobAttacked = false;
    [SerializeField]
    protected bool[] SkillCode;
    [SerializeField]
    float Spell_speed = 5f;
    [SerializeField]
    private GameObject[] Spells;

    private float start_Time;
    private float time;
    private float direction = 2;
    private bool isShooted = false;
    protected void Start() { 
        cam = Camera.main;
    }
    
    public void Update()
    {
        Shoot();
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
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Spells + " " + DoingSpell());
            //Vector2 len = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            //Vector2 dir = new Vector2(len.x, len.y).normalized;
            
            GameObject Spell = Instantiate(Spells[DoingSpell()], transform.position, Quaternion.identity);

            Spell.GetComponent<Rigidbody2D>().velocity = dir_toMouse * Spell_speed;
            isShooted = true;
            End_Shoot(Spell); // 제작중
        }
        
    }

    public virtual void End_Shoot(GameObject Spell)
    {
        if (isShooted) //제작중
        {
            time += Time.deltaTime;
            if (time > direction)
            {
                
                time = 0;
            }
        }
    }
}
