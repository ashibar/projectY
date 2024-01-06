using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStageInfo", menuName = "Scriptable Object/StageInfo", order = int.MaxValue)]
public class StageInfo_so : ScriptableObject
{
    [SerializeField] private string stageName;
    [SerializeField] private Sprite stageSprite;
    [SerializeField] private int difficulty;
    [SerializeField] private Reward_so reward;
    [SerializeField] private StageSort stageSort;
    [SerializeField] private List<EventPhase_so> phases = new List<EventPhase_so>();

    public string StageName { get => stageName; set => stageName = value; }
    public Sprite StageSprite { get => stageSprite; set => stageSprite = value; }
    public Reward_so Reward { get => reward; set => reward = value; }
    public StageSort StageSort { get => stageSort; set => stageSort = value; }
    public List<EventPhase_so> Phases { get => phases; set => phases = value; }
    public int Difficulty { get => difficulty; set => difficulty = value; }
}

public enum StageSort
{
    None,
    Timer,
    targetDestroy,
    targetHp,
    infinite,
}
