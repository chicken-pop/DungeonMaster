using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetirePoint : MonoBehaviour
{
   [SerializeField]
   private GameObject TextModalPrefab;

    private int playerLayer = 0;

    private void Awake()
    {
        TextModalPrefab = Instantiate(TextModalPrefab);
        TextModalPrefab.SetActive(false);
        int layerNo = LayerMask.NameToLayer("Player");
        playerLayer = layerNo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            Debug.Log(collision.name);
            TextModalPrefab.SetActive(true);
            var characters = FindObjectsOfType<CharacterBase>(); //Hieralky上にある関数
            foreach(var character in characters)　　//右辺の配列やリストを変数に格納、左辺を処理したい場合
            {
                character.isActive = false;
                var modal = TextModalPrefab.GetComponent<ModalBase>();
                modal.SetTwoButtonModal("Retirepoint","Do you want to retire?",
                    () => { Debug.Log("yes"); SceneTranditionManager.Instance.SceneLoad("ResultScene");},
                    () => { Debug.Log("no") ;});

            }
            
        }
    }
}
