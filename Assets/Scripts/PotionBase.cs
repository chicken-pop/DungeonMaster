using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBase : ItemBase
{
    [SerializeField]
    private int HealAmount;

    public int GetHealAmount
    {
        get
        {
            return HealAmount;
        }
    }

    public PotionBase(string name,ItemTypes itemType,int healAmount) : base(name, itemType)
    {
        HealAmount = healAmount;
    }
}
