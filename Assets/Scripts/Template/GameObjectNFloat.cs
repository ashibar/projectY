using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectNFloat
{
    [SerializeField] public GameObject obj;
    [SerializeField] public float value;

    public GameObjectNFloat(GameObject obj, float value)
    {
        this.obj = obj;
        this.value = value;
    }
}
