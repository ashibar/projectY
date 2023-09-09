using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// <para/><b>■■ SoundManager ■■</b>
/// <para/>담당자 : 이용욱
/// <para/>요약 : 음원 모듈. 볼륨 설정
/// <para/>비고 : 
/// <para/>업데이트 내역 : 
/// <para/> - (23.09.07) : 문서 생성
/// <para/>
/// </summary>

public class SoundModule : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float volume_default = 0.5f;
    [SerializeField] private float global_volume = 1;
    
    private CancellationTokenSource cts;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetSound(float value, string _name)
    {
        gameObject.name = _name;
        volume_default = value;
        audioSource.volume = volume_default * global_volume; // 옵션에서 변경
    }

    public void BecomeSmaller(float duration)
    {
        cts = new CancellationTokenSource();
        BecomeSmaller_routine(duration, cts);
    }

    public void BecomeLouder(float duration)
    {
        cts = new CancellationTokenSource();
        BecomeLouder_routine(duration, cts);
    }

    private async void BecomeSmaller_routine(float duration, CancellationTokenSource cts)
    {
        float end = Time.time + duration;
        float startValue = audioSource.volume;
        float decreaseAmount;

        while (Time.time < end || !cts.IsCancellationRequested)
        {
            if (audioSource == null) await Task.FromResult(0);
            decreaseAmount = startValue / duration * Time.deltaTime;
            audioSource.volume -= decreaseAmount;
            await Task.Yield();
        }
    }

    private async void BecomeLouder_routine(float duration, CancellationTokenSource cts)
    {
        float end = Time.time + duration;
        float startValue = audioSource.volume;
        float increaseAmount;

        while (Time.time < end || !cts.IsCancellationRequested)
        {
            if (audioSource == null) await Task.FromResult(0);
            increaseAmount = (1 - startValue) / duration * Time.deltaTime;
            audioSource.volume += increaseAmount;
            await Task.Yield();
        }
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
