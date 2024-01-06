using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapStatusWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mapDifficulty;
    [SerializeField] private TextMeshProUGUI mapName;
    [SerializeField] private TextMeshProUGUI mapObjective;
    [SerializeField] private Image mapImage;
    [SerializeField] private Transform spell_holder;

    [SerializeField] private Sprite stage_sprite_default;
    [SerializeField] private GameObject spell_icon_origin;

    private Vector2 baseVector = new Vector2(50f, -50f);
    private float xgap = 110f;
    private float ygap = 110f;

    public void SetStatus(StageInfo_so stageInfo)
    {
        mapName.text = stageInfo.StageName;
        mapDifficulty.text = DifficultyToString(stageInfo.Difficulty);
        mapObjective.text = ObjectiveToString(stageInfo.StageSort);
        mapImage.sprite = stageInfo.StageSprite != null ? stageInfo.StageSprite : stage_sprite_default;

        Spell_Icon[] remain = spell_holder.GetComponentsInChildren<Spell_Icon>();
        for (int i = remain.Length - 1; i >= 0; i--)
            Destroy(remain[i].gameObject);
        if (stageInfo.Reward == null) return;
        List<GameObjectNFloat> rewardList = stageInfo.Reward.RewardList;
        for (int i = 0; i < rewardList.Count; i++)
        {
            GameObjectNFloat reward = rewardList[i];
            GameObject clone = Instantiate(spell_icon_origin, spell_holder);
            Vector2 pos = baseVector + new Vector2(i % 5 * xgap, -(i / 5) * ygap);
            clone.GetComponent<RectTransform>().anchoredPosition = pos;
            clone.GetComponent<Spell_Icon>().SetIcon(reward.obj.GetComponent<Spell>());
        }
    }

    private string ObjectiveToString(StageSort sort)
    {
        switch (sort)
        {
            case StageSort.None:
                return "";
            case StageSort.Timer:
                return "���� �ð����� ����";
            case StageSort.targetDestroy:
                return "��ǥ�� ����";
            case StageSort.targetHp:
                return "";
            case StageSort.infinite:
                return "����";
            default:
                return "";
        }
    }

    private string DifficultyToString(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                return "�Ϲ�";
            case 1:
                return "�����";
            case 2:
                return "������";
            case 3:
                return "�ٸ�Ʈ";
            case 4:
                return "����";
            case 5:
                return "�߸�";
            default:
                return "";
        }
    }
}
