using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ExtraParams para = new ExtraParams();
            para.Name = "isClicked";
            para.Boolvalue = true;
            EventManager.Instance.PostNotification("Set Trigger", this, null, para);
        }
    }
}
