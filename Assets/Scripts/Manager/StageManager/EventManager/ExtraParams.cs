using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExtraParams
{
    private int id;
    private string name;
    private int intvalue;
    private float floatvalue;
    private List<Vector2> vecList = new List<Vector2>();
    private string name2;
    private bool boolvalue;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int Intvalue { get => intvalue; set => intvalue = value; }
    public float Floatvalue { get => floatvalue; set => floatvalue = value; }
    public List<Vector2> VecList { get => vecList; set => vecList = value; }
    public string Name2 { get => name2; set => name2 = value; }
    public bool Boolvalue { get => boolvalue; set => boolvalue = value; }

    public ExtraParams()
    {
        this.id = 0;
        this.name = "";
        this.intvalue = 0;
        this.floatvalue = 0;
        this.vecList= new List<Vector2>();
        this.name = "";
        this.boolvalue = false;
    }

    public ExtraParams(int id, string name, int intvalue, float floatvalue, List<Vector2> vecList, string name2, bool boolvalue)
    {
        this.id = id;
        this.name = name;
        this.intvalue = intvalue;
        this.floatvalue = floatvalue;
        this.vecList = vecList;
        this.name2 = name2;
        this.boolvalue = boolvalue;
    }
}
