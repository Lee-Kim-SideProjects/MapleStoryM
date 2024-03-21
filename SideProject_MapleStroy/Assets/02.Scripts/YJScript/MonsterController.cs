using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rigid;
    private BoxCollider2D monsterCollider;

    //���� �������ͽ�
    public MonsterData monsterData;
    private int maxHp;
    public int currentHp;
    private int monsterLevel;
    private int monsterDamage;
    private int monsterDefensive;
    private int monsterNukback;
    public HealthBar healthBar;

    //���� ��� ������ ������
    public ItemDropTableData dropTableData;
    public LevelController levelController;

    private void Awake()
    {
        // ��ũ��Ʈ �Ҵ�
        levelController = FindObjectOfType<LevelController>();
    }

    private void Start()
    {
        // ������ �� ������ ��������
        maxHp = monsterData.hp;
        currentHp = monsterData.hp;
        monsterLevel = monsterData.level;
        monsterDamage = monsterData.damage;
        monsterDefensive = monsterData.defensive;
        monsterNukback = monsterData.nukback;

        // ü�¹� �ʱ�ȭ
        healthBar.SetHealth(currentHp, maxHp);
    }

    private void Update()
    {
        healthBar.SetHealth(currentHp, maxHp);
        // ������ ü���� 0 ���ϸ� ���� ó��
        if (currentHp <= 0)
            if (!anim.GetBool("isDie"))
                StartCoroutine("Die");
    }
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        monsterCollider = GetComponent<BoxCollider2D>();
        //���� �ʱⰪ���� �ʱ�ȭ
        //HP �ʱ�ȭ
        currentHp = monsterData.hp;
        //HP�� �ʱ�ȭ
        healthBar.SetHealth(currentHp, maxHp);
        //�ݶ��̴� �ѱ�
        monsterCollider.enabled = true;

        if ((rigid.constraints & RigidbodyConstraints2D.FreezePositionX) != 0)
        {
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }
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

    IEnumerator Die()
    {
        //�״� ���
        anim.SetBool("isDie", true);
        // �����̴� ��ũ��Ʈ ����
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        // �״� ����

        // ĳ���� ����ġ �߰�
        PlusMonsterEXP();
        // ������ ���
        DropItem();
        //�ݶ��̴� ����
        monsterCollider.enabled = false;

        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }

    void DropItem()
    {
        //��� ������ ��������
        foreach (ItemDropTableData.DropItems dropItem in dropTableData.items)
        {
            GameObject item = dropItem.dropItem;
            int chance = dropItem.dropChance;
            //������ ��� Ȯ��
            int SuccessItem = Random.Range(1, 100);
            if (chance >= SuccessItem)
            {
                Instantiate(item, transform.position, Quaternion.identity);
            }

            Debug.Log($"Item: {item.name}, Drop Chance: {chance}%");
        }
    }

    void PlusMonsterEXP()
    {
        float PlusEXP = dropTableData.monsterEXP;
        levelController.plusEXP = PlusEXP;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().OnDamaged(this.transform.position);
            StatusManager.Instance.MonsterDamage(monsterDamage);
        }
    }
}