using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CenterText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.enabled = false;
    }

    /// <summary>
    /// <b>화면 가운데에 일정시간 출력 후 자동으로 비활성화 되는 텍스트</b>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="duration"></param>
    /// <param name="fontsize"></param>
    /// <param name="forcequit"></param>
    public void ActiveText(string text, float duration, int fontsize, bool forcequit)
    {
        // 강제 종료
        if (ForceQuit(forcequit)) return;
        
        // 기본값
        if (duration == 0) duration = 3f;
        if (fontsize == 0) fontsize = 70;
        
        // 실행
        StartCoroutine(Render_routine(text, duration, fontsize));
    }

    /// <summary>
    /// <b>forcequit값이 참일 때 CenterText를 강제 비활성화</b>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private bool ForceQuit(bool value)
    {
        if (value)
        {
            tmp.enabled = false;
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// <b>텍스트 출력 서브루틴</b>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="duration"></param>
    /// <param name="fontsize"></param>
    /// <returns></returns>
    private IEnumerator Render_routine(string text, float duration, int fontsize)
    {
        tmp.text = text;
        tmp.fontSize = fontsize;
        tmp.enabled = true;

        yield return new WaitForSeconds(duration);

        tmp.enabled = false;
    }
}
