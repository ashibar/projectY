using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class TopIndicator : MonoBehaviour
{
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private TextMeshProUGUI topText;
    [SerializeField] private Slider targetHPBar;
    [SerializeField] private Unit targetUnit;
    [SerializeField] private StageSort sort = StageSort.None;
    [SerializeField] private float barSpeed = 5;

    public StageSort Sort { get => sort; set => sort = value; }
    public Unit TargetUnit { get => targetUnit; set => targetUnit = value; }

    private void Start()
    {
        unitManager = UnitManager.Instance;
        stageManager = StageManager.Instance;
    }

    private void Update()
    {
        switch (sort)
        {
            case StageSort.None:
                break;
            case StageSort.Timer:
                UpdateTargetIndicator_Timer();
                break;
            case StageSort.targetDestroy:
                UpdateTargetIndicator_DestroyedAmount();
                break;
            case StageSort.targetHp:
                UpdateTargetIndicator_TargetHP();
                break;
        }
    }

    private void UpdateTargetIndicator_Timer()
    {
        float curTime = stageManager.StageEndCheck.CurTime;
        float endTime = stageManager.StageEndCheck.EndTime;
        float remain = endTime - curTime > 0 ? endTime - curTime : 0;

        int min = (int)(remain / 60);
        int sec = (int)(remain % 60);

        string minstr = (min < 10 ? "0" + min.ToString() : min.ToString());
        string secstr = (sec < 10 ? "0" + sec.ToString() : sec.ToString());

        string value = minstr + " : " + secstr;
        topText.text = value;
    }

    private void UpdateTargetIndicator_DestroyedAmount()
    {
        int destroyed = unitManager.TargetDestroyed < unitManager.MaxDestroyed ? unitManager.TargetDestroyed : unitManager.MaxDestroyed;
        string value = destroyed.ToString() + " / " + unitManager.MaxDestroyed.ToString();
        topText.text = value;
    }

    private void UpdateTargetIndicator_TargetHP()
    {
        if (!unitManager.TargetUnit)
            return;
        else
            targetUnit = unitManager.TargetUnit;
        string targetName = targetUnit.stat_so.Name_unit;
        
        float cur = targetUnit.stat.Hp_current;
        float max = targetUnit.stat.Hp;
        //Debug.Log(cur + ", " + max);

        float rate = cur / max;
        targetHPBar.value = Mathf.Lerp(targetHPBar.value, rate, barSpeed * Time.deltaTime);
        topText.text = targetName;
    }

    private void OnValidate()
    {
        SetActive();
    }

    public void SetActive()
    {
        switch (sort)
        {
            case StageSort.None:
                topText.gameObject.SetActive(false);
                targetHPBar.gameObject.SetActive(false);
                break;
            case StageSort.Timer:
                topText.gameObject.SetActive(true);
                targetHPBar.gameObject.SetActive(false);
                topText.text = "00 : 00";
                break;
            case StageSort.targetDestroy:
                topText.gameObject.SetActive(true);
                targetHPBar.gameObject.SetActive(false);
                topText.text = "target / max";
                break;
            case StageSort.targetHp:
                topText.gameObject.SetActive(true);
                targetHPBar.gameObject.SetActive(true);
                topText.text = "Target Enemy";
                break;
        }
    }
}
