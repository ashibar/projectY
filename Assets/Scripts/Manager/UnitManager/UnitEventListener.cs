using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitEventListener : MonoBehaviour
{
    private List<EventMessage> messageBuffer = new List<EventMessage>();
    private UnitManager allGameObj;
    private AimPoint inputData = null;
    private PlayerMovement move = null;
    private ShotManage shot = null;
    
   
    void Start()
    {
        inputData = GetComponent<AimPoint>();
        move  = GetComponent<PlayerMovement>();
        shot = GetComponent<ShotManage>();
        allGameObj = GetComponent<UnitManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EventReciever()
    {
        int error = StageManager.Instance.SearchMassage(3, messageBuffer);
        if (error == -1)
            return;
    }

    private void EventListener()
    {
        foreach (EventMessage m in messageBuffer)
        {
            switch (m.ActionSTR)
            {
                case "Active Move":
                    move.IsMove = true;
                    break;
                case "InActive Move":
                    move.IsMove = false;
                    break;
                case "Active Input":
                    inputData.IsActiveMove = true;
                    move.IsMove = true;
                    shot.IsActiveMagic = true;
                    break;
                case "InActive Input":
                    inputData.IsActiveMove = false;
                    move.IsMove = false;
                    shot.IsActiveMagic = false;
                    break;
                case "Active Magic":
                    shot.IsActiveMagic = true;
                    break;
                case "InActive Magic":
                    shot.IsActiveMagic = false;
                    break;
                case "Force remove":
                    allGameObj.Delete_FromCloneList(gameObject);
                    break;
            }
        }


    }
}
