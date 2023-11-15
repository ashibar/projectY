using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDebugArea : MonoBehaviour
{
    [SerializeField] private SpellPrefabContainer spellPrefab;
    [SerializeField] private PlayerInfoContainer playerInfoContainer;
    [SerializeField] private InventoryWindow inventoryWindow;

    [SerializeField] private List<TMP_Dropdown> dropdowns = new List<TMP_Dropdown>();

    private void Awake()
    {
        playerInfoContainer = LoadDataSingleton.Instance.PlayerInfoContainer();
        spellPrefab = LoadDataSingleton.Instance.SpellPrefabContainer();
        dropdowns.AddRange(GetComponentsInChildren<TMP_Dropdown>());
        Init();
    }

    private void Init()
    {
        foreach (TMP_Dropdown dropdown in dropdowns)
            dropdown.ClearOptions();

        List<string> cores = new List<string>();
        List<string> parts = new List<string>();
        List<string> elements = new List<string>();
        List<string> passives = new List<string>();

        foreach (GameObject obj in spellPrefab.Core) cores.Add(obj.GetComponent<Spell>().name);
        foreach (GameObject obj in spellPrefab.Part) parts.Add(obj.GetComponent<Spell>().name);
        foreach (GameObject obj in spellPrefab.Element) elements.Add(obj.GetComponent<Spell>().name);
        foreach (GameObject obj in spellPrefab.Passive) passives.Add(obj.GetComponent<Spell>().name);

        dropdowns[0].AddOptions(cores);
        dropdowns[1].AddOptions(parts);
        dropdowns[2].AddOptions(elements);
        dropdowns[3].AddOptions(passives);
    }

    public void Press_AddButton(int id)
    {
        switch (id)
        {
            case 0:
                playerInfoContainer.Spell_inventory.Add(new StringNString(spellPrefab.Core[dropdowns[id].value].GetComponent<Spell>().GetCode(), "")); break;
            case 1:
                playerInfoContainer.Spell_inventory.Add(new StringNString(spellPrefab.Part[dropdowns[id].value].GetComponent<Spell>().GetCode(), "")); break;
            case 2:
                playerInfoContainer.Spell_inventory.Add(new StringNString(spellPrefab.Element[dropdowns[id].value].GetComponent<Spell>().GetCode(), "")); break;
            case 3:
                playerInfoContainer.Spell_activated.Add(new StringNString(spellPrefab.Passive[dropdowns[id].value].GetComponent<Spell>().GetCode(), "")); break;
        }
        inventoryWindow.Update_Status();
    }

    public void Press_Update()
    {
        inventoryWindow.Update_Status();
    }

    public void Press_Reset()
    {
        inventoryWindow.Reset_Status();
    }
}
