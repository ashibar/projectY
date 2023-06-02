using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHpText : MonoBehaviour
{
    private string MaxHpText;
    private string CurHpText;
    private TextMeshProUGUI textMeshPro;
    private PlayerHp playerHp;

     void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        playerHp = GetComponentInParent<PlayerHp>();

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
        MaxHpText = playerHp.MaxHp.ToString();
        CurHpText = playerHp.CurHp.ToString();

        textMeshPro.text = CurHpText + "/" + MaxHpText;
    }
}
