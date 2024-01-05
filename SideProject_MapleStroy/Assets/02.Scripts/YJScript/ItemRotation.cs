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
        //���� �����ϱ�
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

    //���� ������
    void OnCollisionEnter2D(Collision2D other)
    {
        // ȸ�� ����
        isGround = true;
        // ���� �ӵ��� �ʱ�ȭ
        rigid.velocity = Vector2.zero;
        // ȸ�� �ʱ�ȭ
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
