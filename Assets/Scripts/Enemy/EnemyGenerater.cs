using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{

    [SerializeField]
    private GameObject[] Enemy;

    private void Start()
    {
        Instantiate(Enemy[0], MapGenerator.EnemyPos, Quaternion.identity);

        Debug.Log(MapGenerator.EnemyPos);
    }
}
