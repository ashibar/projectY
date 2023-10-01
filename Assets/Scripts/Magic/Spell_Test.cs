using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Test : MonoBehaviour
{
    [SerializeField] private Projectile_AnimationModule module;
    [SerializeField] private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        module = GetComponentInChildren<Projectile_AnimationModule>();
        module.SpriteChange_routine();
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        sr.sprite = module.GetSprite();
    }
}
