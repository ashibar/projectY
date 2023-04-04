using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuffSo", menuName = "Scriptable Object/Buff", order = int.MaxValue)]
public class Buff_SO : ScriptableObject
{
    //���ӽð�
    public float duration;
    //����ð�
    public float currentTime;
    //�����̸�
    public string BuffType;
    //���� ��
    public float value;

}
