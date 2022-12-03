using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameterBase : MonoBehaviour
{
    public CharacterParameterBase EnemyParameter;

    [SerializeField]
    private int hitPoint;

    [SerializeField]
    private int attackPoint;

    private void Awake()
    {
        EnemyParameter = new CharacterParameterBase(hitPoint, attackPoint);
    }
}
