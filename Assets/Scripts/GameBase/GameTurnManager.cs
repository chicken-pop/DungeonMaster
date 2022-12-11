using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTurnManager : MonoBehaviour
{
    public static int playerActionCount = 0;

    public static void PlayerActionTurnExecution()
    {  
        playerActionCount++;
    }

  
}
