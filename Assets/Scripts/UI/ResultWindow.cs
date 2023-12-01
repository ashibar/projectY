using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Threading;
using Mono.Cecil.Cil;

public class ResultWindow : AsyncFunction_template
{
    [SerializeField] private StageInfoContainer_so stageInfoContainer;
    
    [SerializeField] private StageClearTextAnimation stage_clear_text_image;
    [SerializeField] private ResultAnimationControl result_animation_control;
    [SerializeField] private CardAnimationControl card_animation_control;

    [SerializeField] public List<Image> spell_card_images;
    [SerializeField] public Button map_button;
    [SerializeField] public Button resume_button;
    [SerializeField] public Button inventory_button;
    [SerializeField] public InventoryWindow inventoryWindow;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private void Awake()
    {
        stageInfoContainer = LoadDataSingleton.Instance.StageInfoContainer();
        stage_clear_text_image = GetComponentInChildren<StageClearTextAnimation>();
        result_animation_control = GetComponentInChildren<ResultAnimationControl>();
        card_animation_control = GetComponentInChildren<CardAnimationControl>();
        //map_button = FindObjectOfType<Button>(true);
    }

    private void Start()
    {
        //Active();
        foreach (SpellCard card in card_animation_control.spell_card)
            card.resultWindow = this;
    }

    public async void Active_Full()
    {
        Time.timeScale = 0;
        PickSpell();
        await stage_clear_text_image.Active(cts.Token);
        await Wait(cts.Token, 3f, false);
        await result_animation_control.Active(cts.Token);
        await card_animation_control.AppearAnimation(cts.Token, false);
        await Wait(cts.Token, 1f, false);
        card_animation_control.SetInteratable(true);
    }

    public async void Active_RewardOnly()
    {
        Time.timeScale = 0;
        PickSpell();
        await card_animation_control.AppearAnimation(cts.Token, false);
        await Wait(cts.Token, 1f, false);
        card_animation_control.SetInteratable(true);
    }

    private void PickSpell()
    {
        float accumulate = 0;
        float random_value = Random.Range(0f, 1f);

        int id = stageInfoContainer.CurID < stageInfoContainer.StageInfoList.Count ? stageInfoContainer.CurID : stageInfoContainer.StageInfoList.Count - 1;
        if (stageInfoContainer.StageInfoList[id].Reward == null) return;
        List<GameObjectNFloat> reward = stageInfoContainer.StageInfoList[id].Reward.RewardList;

        List<Spell> spells = new List<Spell>();

        int min = 0;
        for (int i = 0; i < Mathf.Min(reward.Count, 4); i++)
        {
            if (reward[i].value == -1f)
            {
                Debug.Log(reward[i].obj.GetComponent<Spell>().GetName());
                spells.Add(reward[i].obj.GetComponent<Spell>());
                min++;
            }
        }

        for (int i = min; i < 4; i++)
        {
            random_value = Random.Range(0f, 1f);
            accumulate = 0;
            foreach (GameObjectNFloat r in reward)
            {
                if (r.value == -1f) continue;
                accumulate += r.value;
                Debug.Log(string.Format("{0}, {1}, {2}",i, random_value, accumulate));                
                
                if (random_value <= accumulate)
                {
                    Debug.Log(r.obj.GetComponent<Spell>().GetName());
                    spells.Add(r.obj.GetComponent<Spell>());                    
                    break;
                }
            }
        }
        Debug.Log(spells.Count);
        card_animation_control.SetSpell(spells);
    }

    public void Press_ToTheMap()
    {
        LoadingSceneController.LoadScene("MapScene", stageInfoContainer.CurID);
        Time.timeScale = 1f;
    }

    public void Press_Resume()
    {
        ExtraParams para = new ExtraParams();
        para.Boolvalue = true;
        para.Name = "Player";
        EventManager.Instance.PostNotification("Player Move Input", this, null, para);
        EventManager.Instance.PostNotification("Set Active Spell Manager", this, null, para);
        para.Name = "Enemy";
        EventManager.Instance.PostNotification("Set Active Spell Manager", this, null, para);
        EventManager.Instance.PostNotification("Set Unit AI By Tag", this, null, para);
        LoadDataSingleton.Instance.StageInfoContainer().CurID += 1;

        inventory_button.gameObject.SetActive(false);
        inventoryWindow.gameObject.SetActive(false);
        resume_button.gameObject.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public async void SpellSelected(int id)
    {
        card_animation_control.SetInteratable(false);
        await card_animation_control.SpellSelected(cts.Token, id, false);
        StageSort sort = LoadDataSingleton.Instance.StageInfoContainer().StageInfoList[LoadDataSingleton.Instance.StageInfoContainer().CurID].StageSort;
        if (sort != StageSort.infinite)
        {
            map_button.gameObject.SetActive(true);
            LoadDataSingleton.Instance.PlayerInfoContainer().Progress_step_demo = LoadDataSingleton.Instance.StageInfoContainer().CurID;
        }            
        else
        {
            await Wait(cts.Token, 2f, false);
            foreach (SpellCard card in card_animation_control.spell_card)
                card.ResetSpellAnimation();
            await Wait(cts.Token, 0.1f, false);
            card_animation_control.SetActive(false);
            inventory_button.gameObject.SetActive(true);
            resume_button.gameObject.SetActive(true);
        }        
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
