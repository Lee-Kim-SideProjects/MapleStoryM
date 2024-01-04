using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public enum WeaponType  // 아이템 유형
    {
        OneHandSwoard,
        TwoHandSwoard,
        Bow,
        Stamp
    }

    public WeaponType weapontype;
    public string weaponName;
    public Sprite weaponIcon;
    public Sprite weaponPrefab;
    public int weaponRandomDrop;
    public float weaponDamage;

    [TextArea]
    public string weaponInformation;
}
