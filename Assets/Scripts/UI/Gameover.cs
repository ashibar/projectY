using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    Player player = null;
    void Start()
    {
        player = Player.instance;
    }

    // Update is called once per frame
    
    public void isRetryCheck()
    {
        LoadingSceneController.LoadScene("BattleScene");
    }
    
}
