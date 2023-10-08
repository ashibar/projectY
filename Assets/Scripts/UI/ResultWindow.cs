using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Threading;

public class ResultWindow : AsyncFunction_template
{
    [SerializeField] private StageClearTextAnimation stage_clear_text_image;
    [SerializeField] private ResultAnimationControl result_animation_control;
    [SerializeField] private CardAnimationControl card_animation_control;
    
    [SerializeField] public List<Image> spell_card_images;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private void Awake()
    {
        stage_clear_text_image = GetComponentInChildren<StageClearTextAnimation>();
        result_animation_control = GetComponentInChildren<ResultAnimationControl>();
        card_animation_control = GetComponentInChildren<CardAnimationControl>();
    }

    private void Start()
    {
        //Active();
    }

    public async void Active()
    {
        await stage_clear_text_image.Active(cts.Token);
        await Wait(cts.Token, 3f);
        await result_animation_control.Active(cts.Token);
        await card_animation_control.AppearAnimation(cts.Token);
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
