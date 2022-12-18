using UnityEngine;

public class PlayerParameterBase : CharacterParameterBase
{

    [SerializeField]
    private int playerHitPoint;

    [SerializeField]
    private int playerAttackPoint;

    public float GetPlayerAttackPoint
    {
        get { return playerAttackPoint; }
    }
    private void Awake()
    {
        base.HitPoint = playerHitPoint;
        base.maxHitPoint = base.HitPoint;
        base.AttackPoint = playerAttackPoint;
    }

    public void isDead()
    {
        return;
    }
}
