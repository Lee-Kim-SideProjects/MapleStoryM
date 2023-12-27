using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    private bool attackFinsh = true;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //점프
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !anim.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
            
        //스탑
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.normalized.y * 0.5f);
        }

        //방향 전환
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;

        //애니메이션 전환
        if (Mathf.Abs(rigid.velocity.x) < 0.2)
            anim.SetBool("isWalk", false);
        else
            anim.SetBool("isWalk", true);

        //공격
        BaseAttack();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        //점프 체크
        if (rigid.velocity.y < 0)
        
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.0f, LayerMask.GetMask("Ground")) ;

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.8f)
                    anim.SetBool("isJump", false);
            }
            else
            {
                anim.SetBool("isJump", true);
            }
                
        }
    }

    void BaseAttack()
    {
        if (attackFinsh)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                attackFinsh = false;
                anim.SetTrigger("isAttack");
                Invoke("DelayBaseAttack", 0.9f);
            }
        }
    }
    void DelayBaseAttack()
    {
        attackFinsh = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
            OnDamaged(other.transform.position);
    }

    void OnDamaged(Vector2 targetPos)
    {
        //무적 레이어로 이동
        gameObject.layer = 11;

        //알파값 조절
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //튕겨 나가기 
        int dirc = transform.position.x - targetPos.x > 0 ? 5 : -5;
        rigid.AddForce(new Vector2(dirc, 5), ForceMode2D.Impulse);
        //rigid.AddForce(new Vector2(dirc * 10, 10), ForceMode2D.Impulse);

        Invoke("OffDamaged", 1f);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }
}
