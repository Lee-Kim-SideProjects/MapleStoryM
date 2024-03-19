using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public int nextMove;
    
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        Invoke("Think", 3);
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1.0f, LayerMask.GetMask("Ground"));

        if (rayHit.collider == null)
        {
            Turn();
        } 
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        //È¸Àü
        if(nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }
        anim.SetInteger("MoveSpeed", nextMove);

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        CancelInvoke();
        Invoke("Think", 2);
    }
}
