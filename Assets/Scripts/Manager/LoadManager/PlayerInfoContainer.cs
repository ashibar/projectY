using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoContainer", menuName = "Container/Player Info Container", order = 0)]
public class PlayerInfoContainer : ScriptableObject
{
    [SerializeField] private string player_name;
    [SerializeField] private float money;
    [SerializeField] private List<StringNString> spell_activated = new List<StringNString>();
    [SerializeField] private List<StringNString> spell_inventory = new List<StringNString>();
    [SerializeField] private int progress_step_demo;
    [SerializeField] private int progress_step_infinite;
    [SerializeField] private bool isTutorialCleared;
    [SerializeField] private bool isTutorialAnimCleared;

    public string Player_name { get => player_name; set => player_name = value; }
    public float Money { get => money; set => money = value; }
    public List<StringNString> Spell_activated { get => spell_activated; set => spell_activated = value; }
    public List<StringNString> Spell_inventory { get => spell_inventory; set => spell_inventory = value; }
    public int Progress_step_demo { get => progress_step_demo; set => progress_step_demo = value; }
    public int Progress_step_infinite { get => progress_step_infinite; set => progress_step_infinite = value; }
    public bool IsTutorialCleared { get => isTutorialCleared; set => isTutorialCleared = value; }
    public bool IsTutorialAnimCleared { get => isTutorialAnimCleared; set => isTutorialAnimCleared = value; }

    private void Awake()
    {
        Initiate();
    }

    public void Initiate()
    {
        player_name = "";
        spell_activated.Clear();
        spell_activated.Add(new StringNString("a1", ""));
        spell_activated.Add(new StringNString("c0", "a1"));
        spell_activated.Add(new StringNString("a2", ""));
        spell_inventory.Clear();
        money = 0;
        progress_step_demo = 0;
    }

    /// <summary>
    /// 플레이어 스펠 인벤토리에 스펠 코드 추가
    /// </summary>
    /// <param name="spell_code">추가 스펠</param>
    public void AddSpellToPlayerInfo(StringNString spell_code)
    {
        if (spell_code.string1[0] == 'd')
        {
            spell_activated.Add(spell_code);
        }
        else
        {
            Spell_inventory.Add(spell_code);
        }
    }

    public void AddSpellToPlayerInfo_detailed(StringNString spell_code)
    {
        char sort = spell_code.string1[0];
        if (sort == 'b' || sort == 'c')
        {
            
            for (int i = 0; i < spell_activated.Count; i++)
                if (spell_activated[i].string1[0] == 'a')
                {
                    Debug.Log(string.Format("{0}, {1}", spell_code.string1, spell_activated[i].string1[0]));
                    if (string.Equals(spell_code.string2, spell_activated[i].string1))
                        spell_activated.Insert(i + 1, spell_code);
                }
                    
        }
        else
        {
            spell_activated.Add(new StringNString(spell_code.string1, ""));
        }
    }
}
