using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element_ManagementModule : MonoBehaviour
{
    [SerializeField] private List<GameObject> element_prefabs = new List<GameObject>();

    [SerializeField] private List<Spell_Element> spell_elements = new List<Spell_Element>();
    [SerializeField] private List<StringNNumber> element_level = new List<StringNNumber>();
    [SerializeField] private Spell_Element main_element;

    [SerializeField] private string high_priority;

    private void MixElement()
    {
        foreach (Spell_Element element in spell_elements)
        {
            if (string.Equals(element.level.numberName, "Fire"))
            {
                main_element = element;
                high_priority = "Fire";
            }
            else if (!string.Equals(high_priority, "Fire") && string.Equals(element.level.numberName, "None"))
            {
                main_element = element;
                high_priority = "None";
            }
        }
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
        return main_element;
    }
}
