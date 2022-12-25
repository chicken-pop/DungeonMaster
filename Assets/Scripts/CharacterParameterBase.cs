using UnityEngine;

public class CharacterParameterBase : MonoBehaviour
{
    protected float HitPoint;
    protected float maxHitPoint;
    protected float AttackPoint;
     public float GetAttackPoint
    {
        get { return AttackPoint; }
    }

    public float GetHitPoint
    {
        get { return HitPoint; }
    }

    public float GetMaxHitPoint
    {
        get { return maxHitPoint; }
    }

    public void Damage(float damagePoint)
    {
        this.HitPoint -= damagePoint;
        if (this.HitPoint < 0)
        {
            this.HitPoint = 0;
        }
    }

    public void Heal(float healPoint)
    {
        this.HitPoint += healPoint;
        if (this.HitPoint > this.maxHitPoint)
        {
            this.HitPoint = this.maxHitPoint;
        }
    }

   



}
