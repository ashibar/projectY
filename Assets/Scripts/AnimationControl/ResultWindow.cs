using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultWindow : MonoBehaviour
{
    [SerializeField] public List<Image> spell_card_images;
    
    public void Set_Spell_card_image(int num, Sprite sprite)
    {
        spell_card_images[num].sprite = sprite;
    }

    public void NextStage()
    {
        StageManager.Instance.GoNextStage();
    }
}
