using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingSceneController : MonoBehaviour
{
    private static LoadingSceneController instance;
    public static LoadingSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<LoadingSceneController>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<LoadingSceneController>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<LoadingSceneController>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(LoadingSceneProcess());
    }

    [SerializeField] private TextMeshProUGUI load_progress_text;
    [SerializeField] private Slider load_progress_slider;
    private static string scene_name_to_load;
    private static int stage_no_to_load;
    [SerializeField] private StageInfoContainer_so stageInfoContainer;

    public static void LoadScene(string scene_name, int stage_no = 0)
    {
        scene_name_to_load = scene_name;
        stage_no_to_load = stage_no;
        Debug.Log(scene_name + ", " + stage_no);
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator LoadingSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(scene_name_to_load);
        op.allowSceneActivation = false;

        stageInfoContainer.CurID = stage_no_to_load;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                load_progress_text.text = "Loading...   " + (int)(op.progress * 30) + " %";
                load_progress_slider.value = op.progress * 0.3f;
            }
            else
            {
                timer += Time.unscaledDeltaTime / 1.5f;
                float progress = Mathf.Lerp(0.3f, 1f, timer);
                load_progress_text.text = "Loading...   " + (int)(progress * 100) + " %";
                load_progress_slider.value = progress * 1f;
                if (progress >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    public StageInfoContainer_so GetStageInfoContainer()
    {
        return stageInfoContainer;
    }
}