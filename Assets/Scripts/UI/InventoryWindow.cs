using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private PlayerInfoContainer playerInfoContainer;
    [SerializeField] private SpellPrefabContainer spellPrefabContainer;

    [SerializeField] private TextMeshProUGUI player_name_text;
    [SerializeField] private TextMeshProUGUI player_money_text;

    [SerializeField] private Transform attached_coreicon;
    [SerializeField] private Transform attached_part;
    [SerializeField] private Transform core_tr;
    [SerializeField] private Transform part_tr;

    [SerializeField] private ScrollView_Management attached_scrollview;
    [SerializeField] private ScrollView_Management core_scrollview;
    [SerializeField] private ScrollView_Management part_scrollview;

    [SerializeField] private GameObject spell_icon_origin;
    [SerializeField] private List<GameObject> spell_icon_clone = new List<GameObject>();

    private float xgap = 160f;
    private float ygap = 160f;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Update_Status();
    }

    public void Update_Status()
    {
        if (playerInfoContainer != null)
        {
            player_name_text.text = string.Format("Name : {0}", playerInfoContainer.Player_name);
            player_money_text.text = string.Format("Money : {0}", playerInfoContainer.Money);
            //player_spell_text.text = "Spell List\n";
            List<StringNString> codes_p = playerInfoContainer.Spell_activated;
            List<StringNString> codes_i = playerInfoContainer.Spell_inventory;
            Vector2 baseVector = new Vector2(5f, -5f);
            int core_cnt = 0;
            int part_cnt = 0;

            foreach (GameObject c in spell_icon_clone)
            {
                Destroy(c);
            }
            for (int i = 0; i < codes_p.Count; i++)
            {
                GameObject prefab = spellPrefabContainer.Search(codes_p[i].string1);
                Spell spell = prefab.GetComponent<Spell>();
                switch (codes_p[i].string1[0])
                {
                    case 'a':                        
                        GameObject clone = Instantiate(spell_icon_origin, core_scrollview.content_rt);
                        Vector2 pos = baseVector + new Vector2(0, core_cnt * ygap);
                        clone.GetComponent<RectTransform>().anchoredPosition = pos;
                        clone.GetComponent<Spell_Icon>().SetIcon(spell);
                        spell_icon_clone.Add(clone);
                        core_cnt++;
                        break;
                    case 'b':
                    case 'c':
                    case 'e':
                        clone = Instantiate(spell_icon_origin, attached_scrollview.content_rt);
                        pos = baseVector + new Vector2(part_cnt % 4 * xgap, (part_cnt / 4) * ygap);
                        clone.GetComponent<RectTransform>().anchoredPosition = pos;
                        clone.GetComponent<Spell_Icon>().SetIcon(spell);
                        spell_icon_clone.Add(clone);
                        part_cnt++;
                        break;
                }
            }
            part_cnt = 0;

            for (int i = 0; i < codes_i.Count; i++)
            {
                GameObject prefab = spellPrefabContainer.Search(codes_i[i].string1);
                Spell spell = prefab.GetComponent<Spell>();
                switch (codes_i[i].string1[0])
                {
                    case 'a':
                        break;
                    case 'b':
                    case 'c':
                    case 'e':
                        GameObject clone = Instantiate(spell_icon_origin, part_scrollview.content_rt);
                        Vector2 pos = baseVector + new Vector2(part_cnt % 3 * xgap, (part_cnt / 3) * ygap);
                        clone.GetComponent<RectTransform>().anchoredPosition = pos;
                        clone.GetComponent<Spell_Icon>().SetIcon(spell);
                        spell_icon_clone.Add(clone);
                        part_cnt++;
                        break;
                }
            }
            //player_inventory_text.text = "Inventory\n";
            //foreach (StringNString code in playerInfoContainer.Spell_inventory)
            //{
            //    GameObject prefab = spellPrefabContainer.Search(code.string1);
            //    if (code.string2 != "")
            //        player_spell_text.text += " - ";
            //    player_inventory_text.text += string.Format("{0}\n", prefab.GetComponent<Spell>().GetName());
            //}
        }
    }
}
