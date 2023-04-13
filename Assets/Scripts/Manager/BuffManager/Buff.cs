using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Buff : MonoBehaviour
{
    
    public BuffManager buffmanager;

    private void Awake()
    {
        buffmanager = GetComponentInParent<BuffManager>();
    }

    private void Start()
    {
        StartCoroutine(Destroy_Routine(buff_durationcrrent));
    }
    
    private string buff_name;
    [SerializeField]
    private Buff_SO.BuffType buff_type;
    [SerializeField]
    private float buff_durationcrrent;
    private float buff_currenttime;
    [SerializeField]
    private float buff_value;
    private float buff_redusespeed;
    public Buff(Buff_SO buff)
    {
        this.buff_name = buff.Buff_Name;
        this.buff_type = buff.Buff_Type;
        this.buff_currenttime = buff.Buff_currentTime;
        this.buff_value = buff.Buff_value;
        this.buff_durationcrrent = buff.Buff_duration;
    }

    private void Update()
    {
        
    }

    private IEnumerator Destroy_Routine(float duration)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            yield return null;
        }
        buffmanager.BuffEndListener(this);
        Destroy(gameObject);
    }

    public string Buff_Name { get => buff_name; set => buff_name = value; }
    public Buff_SO.BuffType Buff_Type { get => buff_type; set => buff_type = value; }
    public float Buff_currenttime { get => buff_currenttime; set => buff_currenttime = value; }
    public float Buff_value { get => buff_value; set => buff_value = value;}
    public float Buff_durationcrrent { get => buff_durationcrrent; set => buff_durationcrrent = value;}
}
