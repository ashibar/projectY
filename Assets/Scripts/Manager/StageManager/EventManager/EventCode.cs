using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EventCode
{
    a,
    b,
    c,
    GotoNextPhase,

    // SpawnManager

    ForceSpawn,
    SetActiveSpawner,
    InActiveAll,
    BossSpawn,
    SpawnEnemyAtVectorByID,
    SpawnEnemyAtVectorListByID,
    SpawnEnemyAtVectorByName,

    // UnitManager

    UnitForceMove,
    UnitForceStop,
    PlayerMoveInput,
    PlayerAnimation,
    RegisterPosSearch,

    // UIManager

    FadeOut,
    FadeIn,
    KeyBoardIndicator,
    SetCenterIndicator,
    ForceLoad,

    // ConditionChecker
    
    UnitArrived,

    // Default

    None

}