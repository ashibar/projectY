using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNodeSet : MonoBehaviour
{
    [SerializeField] public MapNode startNode;
    [SerializeField] public StageInfoContainer_so stageInfoContainer;

    private void Awake()
    {
        stageInfoContainer = LoadDataSingleton.Instance.StageInfoContainer();
    }
}
