using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataSingleton : MonoBehaviour
{
    private static LoadDataSingleton instance;
    public static LoadDataSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<LoadDataSingleton>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject("LoadDataSingleton").AddComponent<LoadDataSingleton>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    [SerializeField] private StageInfoContainer_so stageInfoContainer;
    [SerializeField] private PlayerInfoContainer playerInfoContainer;
    [SerializeField] private SpellPrefabContainer spellPrefabContainer;
    [SerializeField] private PrefabSet_so enemyList;

    private void Awake()
    {
        var objs = FindObjectsOfType<LoadDataSingleton>(true);
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
    }

    public StageInfoContainer_so StageInfoContainer()
    {
        if (stageInfoContainer == null)
        {
            stageInfoContainer = Resources.Load("StageInfoContainer_demo") as StageInfoContainer_so;
        }

        return stageInfoContainer;
    }
    public PlayerInfoContainer PlayerInfoContainer()
    {
        if (playerInfoContainer == null)
        {
            playerInfoContainer = Resources.Load("PlayerInfoContainer") as PlayerInfoContainer;
        }

        return playerInfoContainer;
    }
    public SpellPrefabContainer SpellPrefabContainer()
    {
        if (spellPrefabContainer == null)
        {
            spellPrefabContainer = Resources.Load("SpellPrefabContainer") as SpellPrefabContainer;
        }

        return spellPrefabContainer;
    }
    public PrefabSet_so EnemyList()
    {
        if (enemyList == null)
        {
            enemyList = Resources.Load("EnemyList") as PrefabSet_so;
        }

        return enemyList;
    }
    public void SetStageInfoContainer(string sceneName)
    {
        switch (sceneName)
        {
            case "TutorialScene":
                stageInfoContainer = Resources.Load("StageInfoContainer_demo") as StageInfoContainer_so ; break;
            case "BattleScene":
                stageInfoContainer = Resources.Load("StageInfoContainer_infinite") as StageInfoContainer_so;
                playerInfoContainer = Resources.Load("PlayerInfoContainer_infinite") as PlayerInfoContainer; break;
            case "MapScene":
                stageInfoContainer = Resources.Load("StageInfoContainer_demo") as StageInfoContainer_so; break;
        }
    }
}
