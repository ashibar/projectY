using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    public void TestClick()
    {
        LoadingSceneController.LoadScene("BattleScene");
    }
}
