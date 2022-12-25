using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMemoryManager : SingletonMonoBehaviour<DungeonMemoryManager>
{
    private float playerHitPoint = 0f;

    //プロパティ、外からゲットしたい
    public float GetPlayerHitPoint
    {
        get { return playerHitPoint; }
    }

    private float playerMaxHitPoint = 0f;

    public float GetPlayerMaxHitPoint
    {
        get { return playerMaxHitPoint; }
    }

    private float playerAttackPoint = 0f;

      public float GetPlayerAttackPoint
    {
        get { return playerAttackPoint; }
    }

    public void SetPlayerParameter(CharacterParameterBase playerParameter)
    {
        playerHitPoint = playerParameter.GetHitPoint;
        playerMaxHitPoint = playerParameter.GetMaxHitPoint;
        playerAttackPoint = playerParameter.GetAttackPoint;
    }
}
