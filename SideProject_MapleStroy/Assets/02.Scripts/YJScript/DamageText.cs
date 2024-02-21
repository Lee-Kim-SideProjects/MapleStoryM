using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public enum DamageNumber
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten
    }

    //damage
    public void DamageOneByOne(int damage)
    {
        string Sdamage = damage.ToString();

        for (int i = 0; i < Sdamage.Length; i++)
        {
            //answer += (int)Char.GetNumericValue(temp[i]);   
        }
    }
}
