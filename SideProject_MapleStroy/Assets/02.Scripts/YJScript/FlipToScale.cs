using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipToScale : MonoBehaviour
{
    private float h;

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");

        if (h < 0)
            this.transform.localScale = new Vector3(1, 1, 1);
        else if (h > 0)
            this.transform.localScale = new Vector3(-1, 1, 1);
    }
}
