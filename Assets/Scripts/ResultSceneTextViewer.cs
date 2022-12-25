using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultSceneTextViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreText;

    private void Start()
    {
        ScoreText.text = "Score:" + DungeonScoreManager.Instance.dungeonscore.ToString();
    }
}
