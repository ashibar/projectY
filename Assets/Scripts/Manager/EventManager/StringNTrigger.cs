using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringNTrigger
{
    public string triggerName;
    public bool triggerValue;

    public StringNTrigger(string triggerName, bool triggerValue)
    {
        this.triggerName = triggerName;
        this.triggerValue = triggerValue;
    }
}
