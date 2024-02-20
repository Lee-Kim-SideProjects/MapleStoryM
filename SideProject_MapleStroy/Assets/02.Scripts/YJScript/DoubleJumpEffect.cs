using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DoubleJumpEffect : MonoBehaviour
{
    private Transform playerPos;

    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        if (transform.position.x < playerPos.position.x)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnEnable()
    {
        Destroy(this.gameObject, 1.3f);
    }
}
