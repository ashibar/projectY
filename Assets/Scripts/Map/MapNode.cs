using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    [SerializeField] public int index;
    [SerializeField] public bool isAccessable;
    [SerializeField] public MapNode left;
    [SerializeField] public MapNode right;
    [SerializeField] public MapNode up;
    [SerializeField] public MapNode down;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Color c = spriteRenderer.color;
        spriteRenderer.color = new Color(c.r, c.g, c.b, isAccessable ? 1f : 0.5f);
    }
}
