using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public MonsterData monsterData;

    private int currentHp;
    private int monsterLevel;
    private int monsterDefensive;
    private int monsterNukback;

    private void Start()
    {
        // 시작할 때 데이터 가져오기
        currentHp = monsterData.hp;
        monsterLevel = monsterData.level;
        monsterDefensive = monsterData.defensive;
        monsterNukback = monsterData.nukback;
    }

    private void Update()
    {
        // 몬스터의 체력이 0 이하면 죽음 처리
        if (currentHp <= 0)
        {
            Die();
        }
    }

    // 피해를 받는 함수
    public void TakeDamage(int damageAmount)
    {
        // 몬스터의 체력에서 피해만큼 감소
        currentHp -= damageAmount;

        // 체력이 음수가 되지 않도록 보정
        currentHp = Mathf.Max(0, currentHp);
    }

    private void Die()
    {
        // 여기에서 몬스터가 죽을 때의 동작을 구현
        // 캐릭터 경험치 추가
        // 죽는 모션
        // 움직이는 스크립트 정지
        // 죽는 사운드
        Debug.Log(monsterData.monsterName + "이(가) 죽었습니다.");

        gameObject.SetActive(false);
    }
}
