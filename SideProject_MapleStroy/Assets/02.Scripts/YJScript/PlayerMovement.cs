using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float doubleJumpPower;
    private float h;
    
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private bool attackFinsh = true;
    private bool canDoubleJump = false;
    private bool isFront = false; //좌우 확인 여부
    private bool maxSpeedLock = true; //속도 제한 풀기

    public GameObject ctrlEffect;
    public GameObject doubleJumpEffect;
    public Transform doubleJumpEffectPos;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");

        //좌우 체크
        if (h == 1)
            isFront = false;
        else if (h == -1)
            isFront = true;

        //점프
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (!anim.GetBool("isJump"))
            {
                rigid.velocity = Vector2.zero;
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                anim.SetBool("isJump", true);
            }
            else if (canDoubleJump) //더블 점프
            {
                canDoubleJump = false;
                maxSpeedLock = false;

                CreateDJEffect();

                //더블 점프 힘 가하기
                rigid.velocity = Vector2.zero;
                rigid.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
                if (isFront)
                    rigid.AddForce(Vector2.left * doubleJumpPower, ForceMode2D.Impulse);
                else
                    rigid.AddForce(Vector2.right * doubleJumpPower, ForceMode2D.Impulse);
            }
        }

        //스탑
        if (Input.GetButtonUp("Horizontal")) 
        {
            //방향키 떼면 속도 절감
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.normalized.y * 0.5f);
        }

        //방향 전환
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = h == 1;
        }

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
        if (maxSpeedLock)
        {
            //좌우 이동
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            //속도 제한
            if (rigid.velocity.x > maxSpeed)
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxSpeed * (-1))
                rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        //점프 체크
        if (rigid.velocity.y < 0)
        {
            UnityEngine.Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.0f, LayerMask.GetMask("Ground")) ;

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.8f)
                {
                    anim.SetBool("isJump", false);
                    canDoubleJump = true;
                    maxSpeedLock = true;
                }
            }
            else
            {
                anim.SetBool("isJump", true);
            }
                
        }
    }

    void BaseAttack()
    {
        if (attackFinsh && !ctrlEffect.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                ctrlEffect.SetActive(true);
                //모션
                anim.SetTrigger("isAttack");
                //딜레이
                attackFinsh = false;
                Invoke("DelayBaseAttack", 1f);
            }
        }
    }
    void DelayBaseAttack()
    {
        attackFinsh = true;
        ctrlEffect.SetActive(false);
    }

    public void OnDamaged(Vector2 targetPos)
    {
        //무적 레이어로 이동
        gameObject.layer = 11;

        //알파값 조절
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //튕겨 나가기 
        int dirc = transform.position.x - targetPos.x > 0 ? 5 : -5;
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(dirc, 5), ForceMode2D.Impulse);

        Invoke("OffDamaged", 1f);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    void CreateDJEffect()
    {
        Instantiate(doubleJumpEffect, doubleJumpEffectPos.position, Quaternion.identity);
    }
}
