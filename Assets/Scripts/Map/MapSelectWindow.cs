using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectWindow : MonoBehaviour
{
    [SerializeField] private StageInfoContainer_so stageInfoContainer;

    [SerializeField] private MapPlayerControl mapPlayerControl;
    [SerializeField] private MapStatusWindow mapStatusWindow;
    [SerializeField] private MapNodeSet nodeSet;
    [SerializeField] private MapNode currentNode;
    [SerializeField] private int id = 1;

    private bool renderFlag = true;

    private void Awake()
    {
        mapStatusWindow = GetComponentInChildren<MapStatusWindow>();
        currentNode = nodeSet.startNode;
        mapPlayerControl.transform.position = currentNode.transform.position;
        mapStatusWindow.SetStatus(stageInfoContainer.StageInfoList[id]);
    }

    private void Update()
    {
        MovePlayerUI();
    }

    private void MovePlayerUI()
    {
        if (mapPlayerControl.isActive)
        {
            renderFlag = true;
            //mapStatusWindow.gameObject.SetActive(false);
            return;
        }
        else
        {
            //mapStatusWindow.gameObject.SetActive(true);
        }

        PlayerMovement();
    }

    private async void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentNode.left == null)
            {
                Debug.Log("Left is Null");
                return;
            }
            await mapPlayerControl.Move(currentNode.left);
            currentNode = currentNode.left;
            id = currentNode.index;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentNode.right == null)
            {
                Debug.Log("Right is Null");
                return;
            }
            await mapPlayerControl.Move(currentNode.right);
            currentNode = currentNode.right;
            id = currentNode.index;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentNode.up == null)
            {
                Debug.Log("Up is Null");
                return;
            }
            await mapPlayerControl.Move(currentNode.up);
            currentNode = currentNode.up;
            id = currentNode.index;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentNode.down == null)
            {
                Debug.Log("Down is Null");
                return;
            }
            await mapPlayerControl.Move(currentNode.down);
            currentNode = currentNode.down;
            id = currentNode.index;
        }
        stageInfoContainer.CurID = id;
        if (renderFlag)
        {
            mapStatusWindow.SetStatus(stageInfoContainer.StageInfoList[id]);
            renderFlag = false;
        }        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Press_Start();
        }
    }

    public void Press_Start()
    {
        if (mapPlayerControl.isActive) return;
        LoadingSceneController.LoadScene("BattleScene", stageInfoContainer.CurID);
    }
}
