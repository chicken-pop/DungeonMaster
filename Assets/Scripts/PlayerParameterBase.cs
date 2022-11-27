using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameterBase : MonoBehaviour
{
    public CharacterParameterBase PlayerParameter;

    [SerializeField]
    private int hitPoint;

    [SerializeField]
    private int attackPoint;

    private void Awake()
    {
        PlayerParameter = new CharacterParameterBase(hitPoint, attackPoint);
    }
}
