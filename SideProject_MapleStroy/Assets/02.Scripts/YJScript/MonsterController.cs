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
        // ������ �� ������ ��������
        currentHp = monsterData.hp;
        monsterLevel = monsterData.level;
        monsterDefensive = monsterData.defensive;
        monsterNukback = monsterData.nukback;
    }

    private void Update()
    {
        // ������ ü���� 0 ���ϸ� ���� ó��
        if (currentHp <= 0)
        {
            Die();
        }
    }

    // ���ظ� �޴� �Լ�
    public void TakeDamage(int damageAmount)
    {
        // ������ ü�¿��� ���ظ�ŭ ����
        currentHp -= damageAmount;

        // ü���� ������ ���� �ʵ��� ����
        currentHp = Mathf.Max(0, currentHp);
    }

    private void Die()
    {
        // ���⿡�� ���Ͱ� ���� ���� ������ ����
        // ĳ���� ����ġ �߰�
        // �״� ���
        // �����̴� ��ũ��Ʈ ����
        // �״� ����
        Debug.Log(monsterData.monsterName + "��(��) �׾����ϴ�.");

        gameObject.SetActive(false);
    }
}
