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
    /// <b>ȭ�� ����� �����ð� ��� �� �ڵ����� ��Ȱ��ȭ �Ǵ� �ؽ�Ʈ</b>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="duration"></param>
    /// <param name="fontsize"></param>
    /// <param name="forcequit"></param>
    public void ActiveText(string text, float duration, int fontsize, bool forcequit)
    {
        // ���� ����
        if (ForceQuit(forcequit)) return;
        
        // �⺻��
        if (duration == 0) duration = 3f;
        if (fontsize == 0) fontsize = 70;
        
        // ����
        StartCoroutine(Render_routine(text, duration, fontsize));
    }

    /// <summary>
    /// <b>forcequit���� ���� �� CenterText�� ���� ��Ȱ��ȭ</b>
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
    /// <b>�ؽ�Ʈ ��� �����ƾ</b>
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
