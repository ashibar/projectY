using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapStatusWindow_temp : MonoBehaviour
{
    [SerializeField] private StageInfoContainer_so stageInfo_so;

    [SerializeField] private TextMeshProUGUI stage_name_text;
    [SerializeField] private TextMeshProUGUI stage_reward_text;

    private void Awake()
    {
        stageInfo_so = LoadDataSingleton.Instance.StageInfoContainer();
        stage_name_text = GameObject.Find("MapName").GetComponent<TextMeshProUGUI>();
        stage_reward_text = GameObject.Find("StageReward").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Update_Status();
    }

    private void Update_Status()
    {
        int curID = stageInfo_so.CurID;
        stage_name_text.text = string.Format("Stage Name : \n{0}", stageInfo_so.StageInfoList[curID].StageName);
        stage_reward_text.text = "Reward\n";
        if (stageInfo_so.StageInfoList[curID].Reward != null)
            foreach (GameObjectNFloat reward in stageInfo_so.StageInfoList[curID].Reward.RewardList)
                stage_reward_text.text += string.Format("{0}, {1}%\n", reward.obj.GetComponent<Spell>().GetName(), reward.value * 100);
    }

    public void Press_Reset_Button()
    {
        stageInfo_so.CurID = 1;
        Update_Status();
    }

    public void Press_Update_Button()
    {
        Update_Status();
    }
}
