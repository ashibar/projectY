using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusWindow : MonoBehaviour
{
    [SerializeField] private PlayerInfoContainer playerInfoContainer;
    [SerializeField] private SpellPrefabContainer spellPrefabContainer;

    [SerializeField] private TextMeshProUGUI player_name_text;
    [SerializeField] private TextMeshProUGUI player_money_text;
    [SerializeField] private TextMeshProUGUI player_spell_text;

    private void Awake()
    {
        player_name_text = GameObject.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        player_money_text = GameObject.Find("PlayerMoney").GetComponent<TextMeshProUGUI>();
        player_spell_text = GameObject.Find("PlayerSpell").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Update_Status();
    }

    private void Update_Status() {
        if (playerInfoContainer != null)
        {
            player_name_text.text = string.Format("Name : {0}", playerInfoContainer.Player_name);
            player_money_text.text = string.Format("Money : {0}", playerInfoContainer.Money);
            player_spell_text.text = "Spell List\n";
            foreach (StringNString code in playerInfoContainer.Spell_code)
            {
                GameObject prefab = spellPrefabContainer.Search(code.string1);
                if (code.string2 != "")
                    player_spell_text.text += " - ";
                player_spell_text.text += string.Format("{0}\n", prefab.GetComponent<Spell>().GetName());
            }
        }
    }

    public void Press_Reset_Button()
    {
        playerInfoContainer.Initiate();
        Update_Status();
    }

    public void Press_Update_Button()
    {
        Update_Status();
    }
}
