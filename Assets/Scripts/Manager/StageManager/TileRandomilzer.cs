using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomilzer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int index;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Randomize();
    }

    private void Randomize()
    {
        
    }
}
