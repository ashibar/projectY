using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SpellCard_Effect : MonoBehaviour
{
    [SerializeField] private Image card_grid;
    [SerializeField] private Image card_image;
    [SerializeField] private Image card_cover;
    [SerializeField] private float fade_duration = 2;
    [SerializeField] private bool isInterrupted;

    private void Awake()
    {
        //SparklingEffect();
    }

    private void SparklingEffect()
    {
        card_image.gameObject.SetActive(true);
        card_cover.gameObject.SetActive(true);
        FadeOut(fade_duration);
    }

    private async void FadeOut(float duration)
    {
        float end = Time.fixedTime + duration;
        float minus = (1 / duration) / (1 / Time.deltaTime);


        while (Time.fixedTime < end)
        {
            if (isInterrupted)
                await Task.FromResult(0);
            card_cover.color = card_cover.color - new Color(0, 0, 0, minus);
            await Task.Yield();
        }

        await Task.Yield();
    }

    private void OnDestroy()
    {
        isInterrupted = true;
    }
}
