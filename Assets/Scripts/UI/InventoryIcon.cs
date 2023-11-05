using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryIcon : MonoBehaviour
{
    [SerializeField] private GameObject playerInfo_obj;

    public void Press_Button()
    {
        playerInfo_obj.SetActive(!playerInfo_obj.activeSelf);
        if (playerInfo_obj.GetComponentInChildren<InventoryWindow>() != null)
        {
            playerInfo_obj.GetComponentInChildren<InventoryWindow>().Update_Status();
            Debug.Log("Updated");
        }
    }
}
