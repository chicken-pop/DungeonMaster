using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemPotion : MonoBehaviour
{
    public PotionBase Potion;
    
    public PotionBase HighPotion;

    [SerializeField]
    private string potionName = string.Empty;

    [SerializeField]
    private int healAmount;

    private void Awake()
    {
        Potion = new PotionBase(potionName, ItemBase.ItemTypes.Potion,healAmount);
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerParameterBase>())
        {
            var playerParam = collision.gameObject.GetComponent<PlayerParameterBase>();
            playerParam.Heal(Potion.GetHealAmount);
            
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
