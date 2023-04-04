using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Buff : MonoBehaviour
{
    public string bufftype;
    public float duration;
    public float percentage;
    public float currentTime;

    public void Init(string bufftype, float per, float dur)
    {
        this.bufftype = bufftype;
        duration = per;
        percentage = dur;
        currentTime = duration;
        Execute();
    }
    WaitForSeconds seconds = new WaitForSeconds(0.1f);

    public void Execute()
    {
        StartCoroutine(Activation());
    }
    IEnumerator Activation()
    {
        while (currentTime < 0)
        {
            currentTime -= 0.1f;
            yield return seconds;
        }
        currentTime = 0;
        DeActivation();
    }
    public void DeActivation()
    {
        Destroy(gameObject);
    }
}
