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

    [SerializeField] private List<NodeLineRenderer> lrList = new List<NodeLineRenderer>();
    [SerializeField] private List<Vector2> trIndex = new List<Vector2>();
    [SerializeField] private GameObject lr_origin;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRenderer_;

    private void Awake()
    {
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>()[0];
        spriteRenderer_ = GetComponentsInChildren<SpriteRenderer>()[1];
        SetLine();
    }

    private void Update()
    {
        Color c = spriteRenderer.color;
        spriteRenderer.color = new Color(c.r, c.g, c.b, isAccessable ? 1f : 0.5f);
        spriteRenderer_.color = new Color(c.r, c.g, c.b, isAccessable ? 1f : 0.5f);
    }

    private void SetLine()
    {
        foreach (Vector2 tr in trIndex)
        {
            NodeLineRenderer lr = Instantiate(lr_origin, transform).GetComponent<NodeLineRenderer>();
            lr.AddTransform(GetTransform((int)tr.x), GetTransform((int)tr.y));
            lrList.Add(lr);
        }
    }
    
    private Transform GetTransform(int id)
    {
        switch (id)
        {
            case 0: return transform;
            case 1: return left.transform ? left.transform : transform;
            case 2: return right.transform ? right.transform : transform;
            case 3: return up.transform ? up.transform : transform;
            case 4: return down.transform ? down.transform : transform;
            default: return transform;
        }
    }
}
