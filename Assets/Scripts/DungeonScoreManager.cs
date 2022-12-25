using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScoreManager : SingletonMonoBehaviour<DungeonScoreManager>
{
     [SerializeField]
     public int dungeonscore = 0;

    public int GetDungeonscore
    {
        get { return dungeonscore;}
    }

    public void AddDungeonScore(int addscore)
    {
        dungeonscore += addscore;
    }
}
