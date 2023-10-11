using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoContainer", menuName = "Container/Player Info Container", order = 0)]
public class PlayerInfoContainer : ScriptableObject
{
    [SerializeField] private string player_name;
    [SerializeField] private List<StringNString> spell_code = new List<StringNString>();
    [SerializeField] private float money;

    public string Player_name { get => player_name; set => player_name = value; }
    public List<StringNString> Spell_code { get => spell_code; set => spell_code = value; }
    public float Money { get => money; set => money = value; }

    private void Awake()
    {
        Initiate();
    }

    public void Initiate()
    {
        player_name = "";
        spell_code.Clear();
        spell_code.Add(new StringNString("a1", ""));
        spell_code.Add(new StringNString("c0", "a1"));
        spell_code.Add(new StringNString("a2", ""));
        money = 0;
    }
}
