using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    Player player = null;
    void Start()
    {
        player = Player.Instance;
    }

    // Update is called once per frame
    
    public void isRetryCheck()
    {
        Time.timeScale = 1;
        LoadingSceneController.LoadScene("BattleScene");
        
    }
    
}
