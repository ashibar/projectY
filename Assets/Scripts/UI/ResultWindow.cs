using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Threading;

public class ResultWindow : AsyncFunction_template
{
    [SerializeField] private StageInfoContainer_so stageInfoContainer;
    
    [SerializeField] private StageClearTextAnimation stage_clear_text_image;
    [SerializeField] private ResultAnimationControl result_animation_control;
    [SerializeField] private CardAnimationControl card_animation_control;

    [SerializeField] public List<Image> spell_card_images;
    [SerializeField] public Button map_button;

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

    public async void Active()
    {
        PickSpell();
        await stage_clear_text_image.Active(cts.Token);
        await Wait(cts.Token, 3f);
        await result_animation_control.Active(cts.Token);
        await card_animation_control.AppearAnimation(cts.Token);
        await Wait(cts.Token, 1f);
        card_animation_control.SetInteratable(true);
    }

    private void PickSpell()
    {
        float accumulate = 0;
        float random_value = Random.Range(0f, 1f);

        if (stageInfoContainer.StageInfoList[stageInfoContainer.CurID].Reward == null) return;
        List<GameObjectNFloat> reward = stageInfoContainer.StageInfoList[stageInfoContainer.CurID].Reward.RewardList;

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
    }

    public async void SpellSelected(int id)
    {
        card_animation_control.SetInteratable(false);
        await card_animation_control.SpellSelected(cts.Token, id);
        Debug.Log("?");
        map_button.gameObject.SetActive(true);
        LoadDataSingleton.Instance.PlayerInfoContainer().Progress_step = LoadDataSingleton.Instance.StageInfoContainer().CurID;
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
