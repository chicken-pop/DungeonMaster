using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{

    [SerializeField]
    private GameObject Enemy;

    public void EnemySpawn()
    {
        foreach (var pos in MapGenerator.EnemyPos)
        {
            var enemy = Instantiate(Enemy);
            enemy.transform.position = pos;
        }
    }
}
