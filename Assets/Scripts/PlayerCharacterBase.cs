using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterBase : CharacterBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            base.SetArrowState(Arrow.Left);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            base.SetArrowState(Arrow.Up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            base.SetArrowState(Arrow.Down);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            base.SetArrowState(Arrow.Right);
        }
        base.Update();
    }
}
