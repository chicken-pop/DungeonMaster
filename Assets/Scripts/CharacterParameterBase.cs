public class CharacterParameterBase
{
    public float HitPoint;
    private float maxHitPoint;
    public float AttackPoint;

    public CharacterParameterBase(float hitPoint,float attackPoint)
    {
        this.HitPoint = hitPoint;
        this.maxHitPoint = this.HitPoint;
        this.AttackPoint = attackPoint;
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
            this.HitPoint = maxHitPoint;
        }
    }
}
