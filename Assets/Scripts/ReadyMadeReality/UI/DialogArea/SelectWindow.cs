using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ReadyMadeReality
{
    public class SelectWindow : MonoBehaviour
    {
        [SerializeField] private GameObject origin;
        [SerializeField] private List<GameObject> clones = new List<GameObject>();

        public void SetSelectWindow(List<string> list)
        {
            Clear();
            for (int i = 0; i < list.Count; i++)
            {
                GameObject clone = Instantiate(origin, transform.position, Quaternion.identity, transform);
                Vector2 ap = clone.GetComponent<RectTransform>().anchoredPosition;
                clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(ap.x, ap.y - (100 * i - 150));
                clone.GetComponentInChildren<TextMeshProUGUI>().text = list[i];
                clones.Add(clone);
            }
        }

        private void Clear()
        {
            if (clones.Count > 0)
                for (int i = clones.Count - 1; i >= 0; i--)
                {
                    GameObject obj = clones[i];
                    clones.RemoveAt(i);
                    Destroy(obj);
                }

        }
    } 
}
