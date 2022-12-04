using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterBase :  CharacterBase
{
    public override void Update()
    {
        Debug.Log($"player {GameTurnManager.playerAction}");
        if (GameTurnManager.playerAction)
        {
            var rand = Random.Range(0,5);
            switch (rand)
            {
                case 0:
                    base.SetArrowState(Arrow.Left);
                    break;
                case 1:
                    base.SetArrowState(Arrow.Up);
                    break;
                case 2:
                    base.SetArrowState(Arrow.Down);
                    break;
                case 3:
                    base.SetArrowState(Arrow.Right);
                    break;
                case 4:
                    base.IsAttack = true;
                    break;
            }
        }
        base.Update();
    }

}
