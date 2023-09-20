using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected List<GameObject> spell_lower_obj = new List<GameObject>();
    [SerializeField] protected List<Spell> spell_lower = new List<Spell>();

    [SerializeField] protected Vector2 dir_toMove;
    [SerializeField] protected Vector2 dir_toShoot;
    [SerializeField] protected Vector2 pos_toShoot;

    public void Awake()
    {
        
    }

    public void SetVector(Vector2 dir_toMove, Vector2 dir_toShoot, Vector2 pos_toShoot)
    {
        this.dir_toMove = dir_toMove;
        this.dir_toMove = dir_toShoot;
        this.pos_toShoot = pos_toShoot;
    }

    public void RegisterAllFromChildren()
    {
        Spell[] temp = GetComponentsInChildren<Spell>();
        foreach (Spell spell in temp)
        {
            RegisterLowerSpell(spell);
        }
    }

    public void RegisterLowerSpell(Spell spell)
    {
        spell_lower.Add(spell);
        spell_lower_obj.Add(spell.gameObject);
    }
}
