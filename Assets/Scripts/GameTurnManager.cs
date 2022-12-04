using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTurnManager : MonoBehaviour
{
    public static bool playerAction = false;

    public static void PlayerActionTurnExecution()
    {
        Debug.Log("aaaaaaa");
        playerAction = true;
    }

    public static void PlayerActionTurnEnd()
    {
        playerAction = false;
    }
}
