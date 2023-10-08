using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoContainer", menuName = "Container/Player Info Container", order = 0)]
public class PlayerInfoContainer : ScriptableObject
{
    [SerializeField] private string player_name;
    [SerializeField] private List<string> spell_code = new List<string>();
    [SerializeField] private float money;

    public string Player_name { get => player_name; set => player_name = value; }
    public List<string> Spell_code { get => spell_code; set => spell_code = value; }
    public float Money { get => money; set => money = value; }

    private void Awake()
    {
        Initiate();
    }

    public void Initiate()
    {
        player_name = "";
        spell_code.Clear();
        money = 0;
    }
}
