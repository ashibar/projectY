using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element_ManagementModule : MonoBehaviour
{
    [SerializeField] private List<GameObject> element_prefabs = new List<GameObject>();

    [SerializeField] private List<Spell_Element> spell_elements = new List<Spell_Element>();
    [SerializeField] private List<StringNNumber> element_level = new List<StringNNumber>();
    [SerializeField] private Spell_Element main_element;

    [SerializeField] private SpellPrefabContainer spellPrefabContainer;

    [SerializeField] private string high_priority;

    private void MixElement()
    {
        Spell_Element temp = null;
        foreach (Spell_Element element in spell_elements)
        {
            if (string.Equals(element.level.numberName, "Fire"))
            {
                temp = element;
                high_priority = "Fire";
            }
            if (string.Equals(element.level.numberName, "Water"))
            {
                temp = element;
                high_priority = "Water";
            }
            if (string.Equals(element.level.numberName, "Earth"))
            {
                temp = element;
                high_priority = "Earth";
            }
            else if (!string.Equals(high_priority, "Fire") && !string.Equals(high_priority, "Water") && !string.Equals(high_priority, "Earth") && string.Equals(element.level.numberName, "None"))
            {
                temp = element;
                high_priority = "None";
            }
        }
        main_element = temp;
    }

    private void RegisterElementAll()
    {
        spell_elements.AddRange(transform.parent.GetComponentsInChildren<Spell_Element>());
        foreach (Spell_Element element in spell_elements)
        {
            element_level.Add(element.level);
        }
    }

    public void Init()
    {
        RegisterElementAll();
        MixElement();
    }

    public void RegisterElement(Spell_Element element)
    {
        spell_elements.Add(element);
        MixElement();
    }

    public Spell_Element GetElementInfo()
    {
        if (main_element == null)
        {
            GameObject origin = spellPrefabContainer.Search("c0");
            Spell_Element element = Instantiate(origin, transform).GetComponent<Spell_Element>();
            main_element = element;
        }
        //MixElement();
        return main_element;
    }

    public List<Spell_Element> GetElementList()
    {
        return spell_elements;
    }
}
