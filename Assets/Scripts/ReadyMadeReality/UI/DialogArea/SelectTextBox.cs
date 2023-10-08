using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTextBox : MonoBehaviour
{
    [SerializeField] public int index;

    public void PostNotification()
    {
        EventManager.Instance.PostNotification("", this, null, index);
    }
}
