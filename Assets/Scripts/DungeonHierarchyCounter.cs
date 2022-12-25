using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonHierarchyCounter : SingletonMonoBehaviour<DungeonHierarchyCounter>
{
    [SerializeField]
    public int dungeonHierarchyCount = 1;

    public int GetDungeonHierarchyCount
    {
        get
        {
            return dungeonHierarchyCount;
        }
    }

    public void DungeonhierarchyCountUP()
    {
        dungeonHierarchyCount++;
    }
}
