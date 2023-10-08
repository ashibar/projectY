using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellPrefabContainer", menuName = "Container/Spell Prefab Container", order = 0)]
public class SpellPrefabContainer : ScriptableObject
{ 
    [SerializeField] private List<GameObject> core = new List<GameObject>();
    [SerializeField] private List<GameObject> part = new List<GameObject>();
    [SerializeField] private List<GameObject> element = new List<GameObject>();
    [SerializeField] private List<GameObject> passive = new List<GameObject>();

    public GameObject Search(string code)
    {
        char sort = code[0];
        GameObject target;
        switch (sort)
        {
            case 'a': target = SearchFromList(code, core); break;
            case 'b': target = SearchFromList(code, part); break;
            case 'c': target = SearchFromList(code, element); break;
            case 'd': target = SearchFromList(code, passive); break;
            default: Debug.Log(string.Format("Can't find sort error : {0}", sort)); return null;
        }
        return target;
    }

    private GameObject SearchFromList(string code, List<GameObject> list)
    {
        foreach (GameObject obj in list)
            if (string.Equals(obj.GetComponent<Spell>().GetCode(), code))
                return obj;

        Debug.Log(string.Format("Can't find code error : {0}", code));
        return null;
    }
}
