using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "New Monster/monster")]
public class MonsterData : ScriptableObject  // 게임 오브젝트에 붙일 필요 X 
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