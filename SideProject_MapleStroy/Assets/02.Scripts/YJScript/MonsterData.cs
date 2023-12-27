using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "New Monster/monster")]
public class MonsterData : ScriptableObject  // ���� ������Ʈ�� ���� �ʿ� X 
{
    /*
    public enum ItemType  // ������ ����
    {
        Equipment,
        Used,
        Ingredient,
        ETC,
    }
    public ItemType itemType; // ������ ����
    */
    public string monsterName; 
    public GameObject monsterPrefab;

    public int level;
    public int hp;
    public int exp;
    public int speed;
    public int damage;
    public int accarcy;
    public int defensive;
    public int nukback;
}