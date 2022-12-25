using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemTreasureBox : MonoBehaviour
{
    public TreasureBoxBase Treasure;

    [SerializeField]
    private string treasureName = string.Empty;

    [SerializeField]
    private int ScoreAmount;

    private void Awake()
    {
        Treasure = new TreasureBoxBase(treasureName, ItemBase.ItemTypes.Treasure, ScoreAmount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerParameterBase>())
        {
            DungeonScoreManager.Instance.AddDungeonScore(Treasure.GetScoreAmount);
            var transformInt = Vector3Int.FloorToInt(this.transform.position);
            StartCoroutine(EraseItemPotionTile(transformInt));
        }
    }

    IEnumerator EraseItemPotionTile(Vector3Int transformInt)
    {
        yield return new WaitForEndOfFrame();
        this.transform.parent.GetComponent<Tilemap>().SetTile(transformInt,null);
    }
}
