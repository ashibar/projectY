using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameoverWindow : AsyncFunction_template
{
    [SerializeField] private StageInfoContainer_so stageInfoContainer;

    [SerializeField] private GameoverTextAnimation gameoverTextAnimation;

    [SerializeField] public Button map_button;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private void Awake()
    {
        gameoverTextAnimation = GetComponentInChildren<GameoverTextAnimation>(true);
    }

    public async void Active()
    {
        await gameoverTextAnimation.Active(cts.Token);
        await Wait(cts.Token, 2f);
        map_button.gameObject.SetActive(true);
    }

    public void Press_ToTheMap()
    {
        LoadingSceneController.LoadScene("MapScene", stageInfoContainer.CurID);
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
