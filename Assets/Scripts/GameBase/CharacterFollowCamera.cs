using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollowCamera : MonoBehaviour
{
    public PlayerCharacterBase PlayerCharacterBase;

    private void LateUpdate()
    {
        var pos = PlayerCharacterBase.gameObject.transform.position;
        pos.z = -1;
        this.transform.position = pos;

        pos.x = Mathf.Clamp(pos.x, 9, 10);
        pos.y = Mathf.Clamp(pos.y, 5, 14);
        this.transform.position = pos;
    }
}
