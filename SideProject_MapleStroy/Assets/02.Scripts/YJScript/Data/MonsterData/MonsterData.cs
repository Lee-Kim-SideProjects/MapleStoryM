using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "New Monster/monster")]
public class MonsterData : ScriptableObject  // ���� ������Ʈ�� ���� �ʿ� X 
{
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