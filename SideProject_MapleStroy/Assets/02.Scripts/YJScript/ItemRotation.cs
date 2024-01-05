using System.Diagnostics;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    public int rotationSpeed = 700;
    public int jumpForce = 50;

    private bool isGround = false;
    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        //위로 힘가하기
        if (rigid != null)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        ItemRotate();
    }

    void ItemRotate()
    {
        if(!isGround)
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    //땅에 닿으면
    void OnCollisionEnter2D(Collision2D other)
    {
        // 회전 멈춤
        isGround = true;
        // 현재 속도를 초기화
        rigid.velocity = Vector2.zero;
        // 회전 초기화
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
