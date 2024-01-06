using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MobSpawnWindow : MonoBehaviour
{
    [SerializeField] private PrefabSet_so enemyList;
    [SerializeField] private GameObject player_prefab;
    [SerializeField] private GameObject player_obj;

    [SerializeField] private List<TMP_Dropdown> dropdowns = new List<TMP_Dropdown>();
    [SerializeField] private GameObject reset_text;

    [SerializeField] private Vector2 spawn_point = new Vector2(14, 0);
    [SerializeField] private SpawnInfoContainer show_container;

    private void Awake()
    {
        enemyList = LoadDataSingleton.Instance.EnemyList();
        dropdowns.AddRange(GetComponentsInChildren<TMP_Dropdown>());

        Init();
    }

    public void Init()
    {
        foreach (TMP_Dropdown dropdown in dropdowns)
            dropdown.ClearOptions();

        List<string> enemies = new List<string>();

        foreach (GameObject obj in enemyList.prefabs)
            enemies.Add(obj.name);

        dropdowns[0].AddOptions(enemies);
    }

    public void Press_Summon()
    {
        GameObject origin = enemyList.prefabs[dropdowns[0].value];
        Vector2 pos = (Vector2)Player.Instance.transform.position;

        GameObject clone = Instantiate(origin , spawn_point + pos, Quaternion.identity, Holder.enemy_holder);
        UnitManager.Instance.Clones.Add(clone);
    }

    public void Press_Show()
    {
        ExtraParams para = new ExtraParams();
        para.SpawnInfo = show_container;
        EventManager.Instance.PostNotification("Spawn By Spawn Info", this, null, para);
    }

    private bool isWaiting;

    public void Press_Reset()
    {
        if (!isWaiting)
            StartCoroutine(WaitForReset());
    }

    private IEnumerator WaitForReset()
    {
        isWaiting = true;
        
        float end = Time.time + 2f;
        
        ExtraParams para = new ExtraParams();
        para.Boolvalue = false;
        para.Name = "Player";
        EventManager.Instance.PostNotification("Player Move Input", null, null, para);
        EventManager.Instance.PostNotification("Set Active Spell Manager", null, null, para);
        EventManager.Instance.PostNotification("Delete All Enemy", null, null, para);
        reset_text.SetActive(true);

        while (Time.time < end)
            yield return null;

        para.Boolvalue = true;
        EventManager.Instance.PostNotification("Player Move Input", null, null, para);
        EventManager.Instance.PostNotification("Set Active Spell Manager", null, null, para);
        Player.Instance.transform.position = Vector3.zero;
        StageManager.Instance.LoadPlayerSpell();
        reset_text.SetActive(false);

        isWaiting = false;
    }
}
