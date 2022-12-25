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
            var characters = FindObjectsOfType<CharacterBase>(); //Hieralky��ɂ���֐�
            foreach(var character in characters)�@�@//�E�ӂ̔z��⃊�X�g��ϐ��Ɋi�[�A���ӂ������������ꍇ
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
