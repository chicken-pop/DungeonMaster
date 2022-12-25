using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStartManager : MonoBehaviour
{
    private void Start()
    {
        DungeonSoundManager.Instance.PlayerBGM(DungeonSoundManager.BGMType.DungeonBGM);
    }
}
