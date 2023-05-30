using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public static Transform projectile_holder;
    public static Transform enemy_holder;

    private void Awake()
    {
        projectile_holder = GetComponentsInChildren<Transform>()[1];
        enemy_holder = GetComponentsInChildren<Transform>()[2];
    }
}
