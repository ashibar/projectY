using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void TestClick()
    {
        anim.SetTrigger("isOut");
        StartCoroutine(LoadProgress());        
    }

    private IEnumerator LoadProgress()
    {
        yield return new WaitForSeconds(1);
        LoadingSceneController.LoadScene("TutorialScene");
    }

    private void Something()
    {
        Debug.Log("aa");
    }
}
