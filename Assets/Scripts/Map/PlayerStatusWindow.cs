using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusWindow : MonoBehaviour
{
    [SerializeField] public PlayerInfoContainer playerInfoContainer;
    [SerializeField] private SpellPrefabContainer spellPrefabContainer;
    [SerializeField] private MapNodeSet mapNodeSet;

    [SerializeField] private TextMeshProUGUI player_name_text;
    [SerializeField] private TextMeshProUGUI player_money_text;
    [SerializeField] private TextMeshProUGUI player_spell_text;
    [SerializeField] private TextMeshProUGUI player_inventory_text;

    [SerializeField] private TMP_InputField applyNo_inputField;

    private void Awake()
    {
        playerInfoContainer = LoadDataSingleton.Instance.PlayerInfoContainer();
        spellPrefabContainer = LoadDataSingleton.Instance.SpellPrefabContainer();
        player_name_text = GameObject.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        player_money_text = GameObject.Find("PlayerMoney").GetComponent<TextMeshProUGUI>();
        player_spell_text = GameObject.Find("PlayerSpell").GetComponent<TextMeshProUGUI>();
        player_inventory_text = GameObject.Find("PlayerInventory").GetComponent<TextMeshProUGUI>();
        applyNo_inputField = GameObject.Find("ApplyNo").GetComponent<TMP_InputField>();
        
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
            foreach (StringNString code in playerInfoContainer.Spell_activated)
            {
                GameObject prefab = spellPrefabContainer.Search(code.string1);
                if (code.string2 != "")
                    player_spell_text.text += " - ";
                player_spell_text.text += string.Format("{0}\n", prefab.GetComponent<Spell>().GetName());
            }
            player_inventory_text.text = "Inventory\n";
            foreach (StringNString code in playerInfoContainer.Spell_inventory)
            {
                GameObject prefab = spellPrefabContainer.Search(code.string1);
                if (code.string2 != "")
                    player_spell_text.text += " - ";
                player_inventory_text.text += string.Format("{0}\n", prefab.GetComponent<Spell>().GetName());
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

    public void Press_Apply_Button()
    {
        if (playerInfoContainer.Spell_inventory.Count <= 0) {
            Debug.Log("Inventory Empty");
            return;
        }
        string code = applyNo_inputField.text;
        playerInfoContainer.AddSpellToPlayerInfo_detailed(new StringNString(playerInfoContainer.Spell_inventory[0].string1, code));
        playerInfoContainer.Spell_inventory.RemoveAt(0);
        Update_Status();
    }

    public void Press_OpenMapAll()
    {
        List<MapNode> nodes = new List<MapNode>(mapNodeSet.GetComponentsInChildren<MapNode>());

        nodes[1].isAccessable = true;
        nodes[2].isAccessable = true;
        nodes[4].isAccessable = true;
        nodes[5].isAccessable = true;
    }

    public void Press_DebugMap()
    {
        LoadingSceneController.LoadScene("Debug_Scene");
    }
}
