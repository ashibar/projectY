using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject introMenu_obj;
    [SerializeField] private Button tutorial_button;
    [SerializeField] private Button infinite_button;
    [SerializeField] private TextMeshProUGUI pressAnyKey_text;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        if (LoadDataSingleton.Instance.PlayerInfoContainer().IsTutorialCleared)
            infinite_button.interactable = true;
        else
            infinite_button.interactable = false;
    }

    public void SetActiveIntroMenu(bool value)
    {
        pressAnyKey_text.gameObject.SetActive(!value);
        introMenu_obj.SetActive(value);
    }

    public void Press_SceneLoad(string sceneName)
    {
        anim.SetTrigger("isOut");
        LoadDataSingleton.Instance.SetStageInfoContainer(sceneName);

        if (string.Equals(sceneName, "TutorialScene")) {
            LoadDataSingleton.Instance.PlayerInfoContainer().Initiate();
            LoadDataSingleton.Instance.StageInfoContainer().CurID = 0;
            if (!LoadDataSingleton.Instance.PlayerInfoContainer().IsTutorialAnimCleared)
                StartCoroutine(LoadProgress(sceneName));
            else
                StartCoroutine(LoadProgress("MapScene"));
        }
        if (string.Equals(sceneName, "BattleScene"))
        {
            LoadDataSingleton.Instance.PlayerInfoContainer().Progress_step_infinite = 0;
            LoadDataSingleton.Instance.PlayerInfoContainer().Initiate();
            LoadDataSingleton.Instance.StageInfoContainer().CurID = 0;
            StartCoroutine(LoadProgress(sceneName));
        }
    }

    private IEnumerator LoadProgress(string sceneName)
    {
        yield return new WaitForSeconds(1);
        LoadingSceneController.LoadScene(sceneName);
    }

    private void Something()
    {
        Debug.Log("aa");
    }
}
