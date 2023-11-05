using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spell_Icon_Inventory : Spell_Icon, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject spell_icon_shadow_origin;
    [SerializeField] private GameObject clone;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (clone == null)
        {
            clone = Instantiate(spell_icon_shadow_origin, transform);
            if (spell_ != null)
            {
                Color c = background.color;
                clone.GetComponentsInChildren<Image>()[0].color = new Color(c.r, c.g, c.b, 0.5f);
                clone.GetComponentsInChildren<Image>()[1].sprite = image.sprite;
                SpellExplainText.Instance.spell = spell_;
                SpellExplainText.Instance.id = id;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (clone != null)
        {
            clone.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (clone != null)
        {
            Destroy(clone);
        }
    }

}
