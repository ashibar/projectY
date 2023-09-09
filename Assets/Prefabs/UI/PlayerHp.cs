using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Slider hpbar;
    public float MaxHp;
    public float CurHp;
    
     void Start()
    {
        
        hpbar = GetComponent<Slider>();

        MaxHp = 100;
        CurHp = 100;
    }
     void Update()
    {
        PlayerHpUpdate();
    }

    public void PlayerHpUpdate()
    {
        hpbar.value = Mathf.Lerp(hpbar.value,(float) Player.Instance.stat.Hp_current/ (float)Player.Instance.stat.Hp, speed * Time.deltaTime);
    }

}