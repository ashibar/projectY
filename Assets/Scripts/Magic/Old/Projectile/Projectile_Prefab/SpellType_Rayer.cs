using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellType_Rayer : MonoBehaviour
{
    [SerializeField] public GameObject SpellPrefab;
    [SerializeField] BoxCollider2D razer;
    [SerializeField] public float Delay = 1f;
    [SerializeField] public float Duration = 200f;
    [SerializeField] public float razerSize_x, razerSize_y;
    [SerializeField] public float offset_x, offset_y;
    private void Start()
    {
        razer = GetComponent<BoxCollider2D>();
        
    }
    private IEnumerator RazerStart()
    {
        yield return new WaitForSeconds(Delay);
        razer.size = new Vector2(razerSize_x, razerSize_y);
        Vector2 offset = new Vector2(offset_x, offset_y);
        razer.offset = offset;

        yield return new WaitForSeconds(Duration);


    }

}
