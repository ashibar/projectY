using ReadyMadeReality;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExtraParams
{
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private int intvalue;
    [SerializeField] private float floatvalue;
    [SerializeField] private List<Vector2> vecList = new List<Vector2>();
    [SerializeField] private List<GameObject> mobLists = new List<GameObject>();
    [SerializeField] private string name2;
    [SerializeField] private bool boolvalue;
    [SerializeField] private EventPhase_so nextPhase;
    [SerializeField] private DialogInfo_so dialog_so;
    [SerializeField] private AudioClip audioclip;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int Intvalue { get => intvalue; set => intvalue = value; }
    public float Floatvalue { get => floatvalue; set => floatvalue = value; }
    public List<Vector2> VecList { get => vecList; set => vecList = value; }
    public List<GameObject> MobLists { get => mobLists; set => mobLists = value; }
    public string Name2 { get => name2; set => name2 = value; }
    public bool Boolvalue { get => boolvalue; set => boolvalue = value; }
    public EventPhase_so NextPhase { get => nextPhase; set => nextPhase = value; }
    public DialogInfo_so Dialog_so { get => dialog_so; set => dialog_so = value; }
    public AudioClip Audioclip { get => audioclip; set => audioclip = value; }

    public ExtraParams()
    {
        this.id = 0;
        this.name = "";
        this.intvalue = 0;
        this.floatvalue = 0;
        this.vecList= new List<Vector2>();
        this.mobLists = new List<GameObject>();
        this.name = "";
        this.boolvalue = false;
        this.nextPhase = null;
        this.dialog_so = null;
        this.audioclip = null;
        
    }

    public ExtraParams(int id, string name, int intvalue, float floatvalue, List<Vector2> vecList, List<GameObject> mobLists, string name2, bool boolvalue, EventPhase_so nextPhase, DialogInfo_so dialog_so, AudioClip audioclip)
    {
        this.id = id;
        this.name = name;
        this.intvalue = intvalue;
        this.floatvalue = floatvalue;
        this.vecList = vecList;
        this.mobLists = mobLists;
        this.name2 = name2;
        this.boolvalue = boolvalue;
        this.nextPhase = nextPhase;
        this.dialog_so = dialog_so;
        this.audioclip = audioclip;
    }
}
