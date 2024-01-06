using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDropTableData : ScriptableObject
{
    [System.Serializable]
    public class DropItems
    {
        public GameObject dropItem;
        public int dropChance;
    }
    public List<DropItems> items = new List<DropItems>();

    public float monsterEXP;
}