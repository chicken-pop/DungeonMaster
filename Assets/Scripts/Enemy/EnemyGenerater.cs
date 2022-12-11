using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    public GameObject[] EnemyÅ@= new GameObject[2];

    public void EnemySpawn(Vector2 spawnPos,EnemyParameterBase.EnemyType enemyType)
    {
        foreach (var pos in MapGenerator.EnemyPos)
        {
            var enemy = Instantiate(Enemy[(int)enemyType]);
            enemy.transform.position = spawnPos;
        }
    }
}
