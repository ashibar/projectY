using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Reward", menuName = "Scriptable Object/Reward SO", order = int.MaxValue)]
public class Reward_so : ScriptableObject
{
    [SerializeField] private List<GameObjectNFloat> rewardList = new List<GameObjectNFloat>();

    public List<GameObjectNFloat> RewardList { get => rewardList; set => rewardList = value; }
}
