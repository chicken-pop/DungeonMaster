using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStagePos : MonoBehaviour
{
    int playerLayer = 0;

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            DungeonScoreManager.Instance.AddDungeonScore(5);

            DungeonMemoryManager.Instance.SetPlayerParameter(collision.gameObject.GetComponent<CharacterParameterBase>());

            DungeonHierarchyCounter.Instance.DungeonhierarchyCountUP();

            SceneTranditionManager.Instance.SceneLoad("SampleScene");
        }
    }
}
