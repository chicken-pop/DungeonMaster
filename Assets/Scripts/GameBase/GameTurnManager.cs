using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTurnManager : MonoBehaviour
{
    public static bool playerAction = false;

    public static void PlayerActionTurnExecution()
    {
       
        playerAction = true;
    }

    public static void PlayerActionTurnEnd()
    {
        playerAction = false;
    }
}
