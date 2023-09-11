using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStageInfo", menuName = "Scriptable Object/StageInfo", order = int.MaxValue)]
public class StageInfo_so : ScriptableObject
{
    [SerializeField] private string stageName;
    [SerializeField] private List<GameObject> rewards = new List<GameObject>();
    [SerializeField] private StageSort stageSort;
    [SerializeField] private List<EventPhase_so> phases = new List<EventPhase_so>();

    public string StageName { get => stageName; set => stageName = value; }
    public List<GameObject> Rewards { get => rewards; set => rewards = value; }
    public StageSort StageSort { get => stageSort; set => stageSort = value; }
    public List<EventPhase_so> Phases { get => phases; set => phases = value; }    
}

public enum StageSort
{
    None,
    Timer,
    targetDestroy,
    targetHp
}
