using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class CharacterBase : MonoBehaviour
{
    public enum Arrow
    {
        Invalide = -1,
        Left,
        Up,
        Right,
        Down,
    }

    public Arrow Arrows;

    private Animator characterAnimator = null;

    private const string Walk = "Walk";
    private const string Attack = "Attack";


    private void Awake()
    {
        characterAnimator = this.gameObject.GetComponentInChildren<Animator>();
    }

    public virtual void Update()
    {
        var FloorToIntPos = Vector3Int.FloorToInt(this.transform.position);

        if (this.transform.position != FloorToIntPos)
        {
            this.transform.position = FloorToIntPos;
        }

        switch (Arrows)
        {
            case Arrow.Invalide:
                break;

            case Arrow.Left:
                //左に移動
                if (CheckPos(FloorToIntPos += Vector3Int.left))
                {
                    this.transform.position += Vector3Int.left;
                    AnimationExecution(Walk, Vector3Int.left);
                }
                break;

            case Arrow.Up:
                //上に移動
                if (CheckPos(FloorToIntPos += Vector3Int.up))
                {
                    this.transform.position += Vector3Int.up;
                    AnimationExecution(Walk, Vector3Int.up);
                }
                break;

            case Arrow.Right:
                //右に移動
                if (CheckPos(FloorToIntPos += Vector3Int.right))
                {
                    this.transform.position += Vector3Int.right;
                    AnimationExecution(Walk, Vector3Int.right);
                }
                break;

            case Arrow.Down:
                //下に移動
                if (CheckPos(FloorToIntPos += Vector3Int.down))
                {
                    this.transform.position += Vector3Int.down;
                    AnimationExecution(Walk, Vector3Int.down);
                }
                break;
        }

        Arrows = Arrow.Invalide;

        //追加
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AnimationAttack();
        }
    }

    //継承先が限定しているとき（publicでもできるけど…）
    protected void SetArrowState(Arrow arrow)
    {
        Arrows = arrow;
    }

    private void AnimationExecution(string animationName,Vector3Int direction)
    {
        characterAnimator.SetBool(animationName, true);
        characterAnimator.SetFloat("X", direction.x);
        characterAnimator.SetFloat("Y", direction.y);
        characterAnimator.SetTrigger("Clicked");
    }

    //追加
    void AnimationAttack()
    {
        characterAnimator.SetBool(Attack, true);
        characterAnimator.SetTrigger("Clicked");
        StartCoroutine(OneAttack());
        
    }

    //追加
    IEnumerator OneAttack()
    {
        yield return new WaitForSeconds(0.3f);
        characterAnimator.SetBool(Attack, false);
        characterAnimator.SetTrigger("Clicked");
    }

    private bool CheckPos(Vector3 vec)
    {
        if (MapGenerator.map[(int)vec.x, (int)vec.y] == 1)
        {
            return false;
        }
        return true;
    }



}
