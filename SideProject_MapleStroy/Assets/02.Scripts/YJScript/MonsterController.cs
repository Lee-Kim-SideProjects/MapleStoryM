using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rigid;
    private BoxCollider2D monsterCollider;

    //몬스터 스테이터스
    public MonsterData monsterData;
    private int maxHp;
    public int currentHp;
    private int monsterLevel;
    private int monsterDamage;
    private int monsterDefensive;
    private int monsterNukback;
    public HealthBar healthBar;

    //몬스터 드랍 아이템 데이터
    public ItemDropTableData dropTableData;
    public LevelController levelController;

    private void Awake()
    {
        // 스크립트 할당
        levelController = FindObjectOfType<LevelController>();
    }

    private void Start()
    {
        // 시작할 때 데이터 가져오기
        maxHp = monsterData.hp;
        currentHp = monsterData.hp;
        monsterLevel = monsterData.level;
        monsterDamage = monsterData.damage;
        monsterDefensive = monsterData.defensive;
        monsterNukback = monsterData.nukback;

        // 체력바 초기화
        healthBar.SetHealth(currentHp, maxHp);
    }

    private void Update()
    {
        healthBar.SetHealth(currentHp, maxHp);
        // 몬스터의 체력이 0 이하면 죽음 처리
        if (currentHp <= 0)
            if (!anim.GetBool("isDie"))
                StartCoroutine("Die");
    }
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        monsterCollider = GetComponent<BoxCollider2D>();
        //몬스터 초기값으로 초기화
        //HP 초기화
        currentHp = monsterData.hp;
        //HP바 초기화
        healthBar.SetHealth(currentHp, maxHp);
        //콜라이더 켜기
        monsterCollider.enabled = true;

        if ((rigid.constraints & RigidbodyConstraints2D.FreezePositionX) != 0)
        {
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }
    }

    // 피해를 받는 함수
    public void TakeDamage(int damageAmount)
    {
        // 몬스터의 체력에서 피해만큼 감소
        currentHp -= damageAmount;

        // 체력이 음수가 되지 않도록 보정
        currentHp = Mathf.Max(0, currentHp);

        //체력바 스크립트 호출
        healthBar.SetHealth(currentHp, maxHp);
    }

    IEnumerator Die()
    {
        //죽는 모션
        anim.SetBool("isDie", true);
        // 움직이는 스크립트 정지
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        // 죽는 사운드

        // 캐릭터 경험치 추가
        PlusMonsterEXP();
        // 아이템 드랍
        DropItem();
        //콜라이더 끄기
        monsterCollider.enabled = false;

        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }

    void DropItem()
    {
        //드랍 데이터 가져오기
        foreach (ItemDropTableData.DropItems dropItem in dropTableData.items)
        {
            GameObject item = dropItem.dropItem;
            int chance = dropItem.dropChance;
            //아이템 드랍 확률
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