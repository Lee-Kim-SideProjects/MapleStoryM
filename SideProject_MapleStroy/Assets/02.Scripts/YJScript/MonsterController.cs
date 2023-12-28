using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public MonsterData monsterData;
    public HealthBar healthBar;
    Animator anim;
    Rigidbody2D rigid;

    private int maxHp;
    public int currentHp;
    private int monsterLevel;
    private int monsterDefensive;
    private int monsterNukback;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        // ������ �� ������ ��������
        maxHp = monsterData.hp;
        currentHp = monsterData.hp;
        monsterLevel = monsterData.level;
        monsterDefensive = monsterData.defensive;
        monsterNukback = monsterData.nukback;

        //ü�¹� �ʱ�ȭ
        healthBar.SetHealth(currentHp, maxHp);
    }

    private void Update()
    {
        healthBar.SetHealth(currentHp, maxHp);
        // ������ ü���� 0 ���ϸ� ���� ó��
        if (currentHp <= 0)
            if (!anim.GetBool("isDie"))
                Die();
    }
    private void OnEnable()
    {
        //�� �ʱ�ȭ
        currentHp = monsterData.hp;
        gameObject.GetComponent<MonsterMovement>().enabled = true;
        healthBar.SetHealth(currentHp, maxHp);
    }

    // ���ظ� �޴� �Լ�
    public void TakeDamage(int damageAmount)
    {
        // ������ ü�¿��� ���ظ�ŭ ����
        currentHp -= damageAmount;

        // ü���� ������ ���� �ʵ��� ����
        currentHp = Mathf.Max(0, currentHp);

        //ü�¹� ��ũ��Ʈ ȣ��
        healthBar.SetHealth(currentHp, maxHp);
    }

    void Die()
    {
        // ĳ���� ����ġ �߰�

        //�״� ���
        anim.SetBool("isDie",true);
        // �����̴� ��ũ��Ʈ ����
       // gameObject.GetComponent<MonsterMovement>().enabled = false;
        // �״� ����

        Invoke("OffDieSnail", 2f);
    }

    void OffDieSnail()
    {
        gameObject.SetActive(false);
    }
}
