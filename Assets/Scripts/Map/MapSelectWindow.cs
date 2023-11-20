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
        stageInfoContainer = LoadDataSingleton.Instance.StageInfoContainer();
        mapStatusWindow = GetComponentInChildren<MapStatusWindow>();
    }

    private void Start()
    {
        currentNode = nodeSet.startNode;
        id = nodeSet.startNode.index;
        mapStatusWindow.SetStatus(stageInfoContainer.StageInfoList[id]);
    }

    private void Update()
    {
        MovePlayerUI();
    }

    private void MovePlayerUI()
    {
        if (mapPlayerControl.isMoving)
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
        if (mapPlayerControl.isActive == false)
            return;
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentNode.left == null)
            {
                Debug.Log("Left is Null");
                return;
            }
            if (!currentNode.left.isAccessable)
            {
                Debug.Log("Can't Access");
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
            if (!currentNode.right.isAccessable)
            {
                Debug.Log("Can't Access");
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
            if (!currentNode.up.isAccessable)
            {
                Debug.Log("Can't Access");
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
            if (!currentNode.down.isAccessable)
            {
                Debug.Log("Can't Access");
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
        if (mapPlayerControl.isMoving) return;
        LoadingSceneController.LoadScene("BattleScene", stageInfoContainer.CurID);
    }
}
