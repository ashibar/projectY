using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EventCode
{
    a,
    b,
    c,
    d,

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

    // UIManager

    FadeOut,
    FadeIn,
    KeyBoardIndicator,
    SetCenterIndicator,
    ForceLoad,

    // Default

    None

}