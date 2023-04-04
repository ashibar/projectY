using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuffSo", menuName = "Scriptable Object/Buff", order = int.MaxValue)]
public class Buff_SO : ScriptableObject
{
    //지속시간
    public float duration;
    //현재시간
    public float currentTime;
    //버프이름
    public string BuffType;
    //버프 값
    public float value;

}
