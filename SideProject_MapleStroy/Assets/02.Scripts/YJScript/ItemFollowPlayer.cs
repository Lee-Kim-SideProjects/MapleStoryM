using UnityEngine;

public class ItemFollowPlayer : MonoBehaviour
{
    public Transform targetPosition;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private float fadeCount = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            targetPosition = playerObject.transform;
        }
    }

    void Update()
    {
        FollowPlayer();
        FadeOut();
    }

    //�÷��̾� ���󰡱�
    void FollowPlayer()
    {
        rigid.bodyType = RigidbodyType2D.Kinematic;
        transform.position = Vector3.Slerp(transform.position, targetPosition.transform.position, 0.06f);
    }

    //������ �������
    void FadeOut()
    {
        fadeCount -= 0.01f;
        spriteRenderer.color = new Color(1, 1, 1, fadeCount);
    }
}
