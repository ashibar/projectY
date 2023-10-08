using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkipButton : MonoBehaviour
{
    public void Press_Skip()
    {
        LoadingSceneController.LoadScene("MapScene", 1);
    }
}
