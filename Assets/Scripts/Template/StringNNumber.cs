using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringNNumber
{
    public string numberName;
    public float numberValue;

    public StringNNumber(string numberName, float numberValue)
    {
        this.numberName = numberName;
        this.numberValue = numberValue;
    }
}
