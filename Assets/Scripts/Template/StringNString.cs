using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringNString
{
    [SerializeField] public string string1 = "";
    [SerializeField] public string string2 = "";

    public StringNString(string string1, string string2)
    {
        this.string1 = string1;
        this.string2 = string2;
    }
}
