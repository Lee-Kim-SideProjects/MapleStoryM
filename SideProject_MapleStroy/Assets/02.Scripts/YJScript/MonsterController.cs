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

        // 시작할 때 데이터 가져오기
        maxHp = monsterData.hp;
        currentHp = monsterData.hp;
        monsterLevel = monsterData.level;
        monsterDefensive = monsterData.defensive;
        monsterNukback = monsterData.nukback;

        //체력바 초기화
        healthBar.SetHealth(currentHp, maxHp);
    }

    private void Update()
    {
        healthBar.SetHealth(currentHp, maxHp);
        // 몬스터의 체력이 0 이하면 죽음 처리
        if (currentHp <= 0)
            if (!anim.GetBool("isDie"))
                Die();
    }
    private void OnEnable()
    {
        //값 초기화
        currentHp = monsterData.hp;
        gameObject.GetComponent<MonsterMovement>().enabled = true;
        healthBar.SetHealth(currentHp, maxHp);
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

    void Die()
    {
        // 캐릭터 경험치 추가

        //죽는 모션
        anim.SetBool("isDie",true);
        // 움직이는 스크립트 정지
       // gameObject.GetComponent<MonsterMovement>().enabled = false;
        // 죽는 사운드

        Invoke("OffDieSnail", 2f);
    }

    void OffDieSnail()
    {
        gameObject.SetActive(false);
    }
}
