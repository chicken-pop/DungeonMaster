using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterBase :  CharacterBase
{
    private bool isChase= false;

    private int chaseDirection = 0;
    float playerDiff = 10f;

    private int enemyActionCount = 0;

    private void Start()
    {
        this.transform.position = MapGenerator.EnemyPos[0];
        base.isEnemy = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerParameterBase>())
        {
            Debug.Log("aaa");

            isChase = true;
            Vector3 v = (collision.transform.position - this.transform.position).normalized;
            playerDiff = (collision.transform.position - this.transform.position).magnitude;
            var face = Vector3Int.zero;

            if(v.x < 0)
            {
                chaseDirection = (int)Arrow.Left;
                face = Vector3Int.left;
            }
            if(v.x > 0)
            {
                chaseDirection = (int)Arrow.Right;
                face = Vector3Int.right;
            }
            if(v.y < 0)
            {
                chaseDirection = (int)Arrow.Down;
                face = Vector3Int.down;
            }
            if(v.y > 0)
            {
                chaseDirection = (int)Arrow.Left;
                face = Vector3Int.up;
            }
            if(playerDiff <= 2)
            {
                base.LookToDirection(face);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerParameterBase>())
        {
            isChase = false;

            Debug.Log("bbb");
        }
    }

    public override void Update()
    {
        if (GameTurnManager.playerActionCount != enemyActionCount)
        {
            //++�̂��ƃC���N�������g�A--�̂��ƃf�B�N�������g
            enemyActionCount++;

            if (isChase)
            {
                if(playerDiff <= 2)
                {
                    base.IsAttack = true;
                }
                else
                {
                    base.SetArrowState((Arrow)chaseDirection);
                }
            }
            else
            {
                var rand = Random.Range(0,4);
            switch (rand)
            {
                case 0:
                    base.SetArrowState(Arrow.Left);
                    break;
                case 1:
                    base.SetArrowState(Arrow.Up);
                    break;
                case 2:
                    base.SetArrowState(Arrow.Right);
                    break;
                case 3:
                    base.SetArrowState(Arrow.Down);
                    break;
            }
        }
     }
     base.Update();
    }
}
