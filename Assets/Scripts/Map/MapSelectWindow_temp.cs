using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectWindow_temp : MonoBehaviour
{
    [SerializeField] private StageInfoContainer_so stage_container;
    
    [SerializeField] private Button left_button;
    [SerializeField] private Button right_button;
    [SerializeField] private Image stage_image;
    [SerializeField] private Button stage_image_button;
    [SerializeField] private TextMeshProUGUI stage_name;

    private void Awake()
    {
        left_button = GameObject.Find("LeftArrow").GetComponent<Button>();
        right_button = GameObject.Find("RightArrow").GetComponent<Button>();
        stage_image = GameObject.Find("CenterImage").GetComponent<Image>();
        stage_image_button = GameObject.Find("CenterImage").GetComponent<Button>();
        stage_name = GameObject.Find("StageName").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Update_StageInfo();
    }

    private void Update()
    {
        UpdateButtonActivation();        
    }

    private void UpdateButtonActivation()
    {
        int curID = stage_container.CurID;

        if (curID == 1)
        {
            left_button.interactable = false;
            right_button.interactable = true;
        }
        else if (curID == stage_container.StageInfoList.Count - 1)
        {
            left_button.interactable = true;
            right_button.interactable = false;
        }
        else
        {
            left_button.interactable = true;
            right_button.interactable = true;
        }
    }

    public void Press_LeftButton()
    {
        int curID = stage_container.CurID;

        if (curID > 1)
        {
            stage_container.CurID -= 1;
            Update_StageInfo();
        }
    }

    public void Press_RightButton()
    {
        int curID = stage_container.CurID;

        if (curID < stage_container.StageInfoList.Count - 1)
        {
            stage_container.CurID += 1;
            Update_StageInfo();
        }
    }

    public void Press_CenterImageButton()
    {
        LoadingSceneController.LoadScene("BattleScene", stage_container.CurID);
    }

    private void Update_StageInfo()
    {
        stage_name.text = stage_container.StageInfoList[stage_container.CurID].name;
    }
}
