using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField]
    Button startButton;

    private void Start()
    {
        DungeonSoundManager.Instance.PlayerBGM(DungeonSoundManager.BGMType.DungeonTitleBGM);

        DungeonScoreManager.Instance.DungeonScoreInit();

        startButton.onClick.AddListener(()=>{ SceneTranditionManager.Instance.SceneLoad("SampleScene");});

        startButton.onClick.AddListener(A);

        void A() { 
            }
    }
}
