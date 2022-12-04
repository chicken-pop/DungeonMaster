using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{

    [SerializeField]
    private GameObject[] Enemy;

    private void Start()
    {
       StartCoroutine(enemyInstantiate());
        Debug.Log(MapGenerator.EnemyPos);
    }

    IEnumerator enemyInstantiate() { 
         yield return new WaitWhile(()=>MapGenerator.EnemyPos == Vector3.zero);
        yield return new WaitUntil(()=>MapGenerator.EnemyPos != Vector3.zero);
         Instantiate(Enemy[0], MapGenerator.EnemyPos, Quaternion.identity);

        
        }
}
