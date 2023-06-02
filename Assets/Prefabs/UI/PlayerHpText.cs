using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHpText : MonoBehaviour
{
    private string MaxHpText;
    private string CurHpText;
    private TextMeshProUGUI textMeshPro;

     void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

    }

    void Start()
    {
        UpdateHpText();
    }

     void Update()
    {
        UpdateHpText();
    }

    public void UpdateHpText()
    {
        MaxHpText = Player.Instance.stat.Hp.ToString();
        if (Player.Instance.stat.Hp_current >= 0)
            CurHpText = Player.Instance.stat.Hp_current.ToString();
        else
            CurHpText = "0";

        textMeshPro.text = CurHpText + "/" + MaxHpText;
    }
}
