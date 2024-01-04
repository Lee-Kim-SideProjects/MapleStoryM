using UnityEngine;

public class ItemFadeOut : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float fadeCount = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FadeOut();
    }

    void FadeOut()
    {
        fadeCount -= 0.01f;
        spriteRenderer.color = new Color(1, 1, 1, fadeCount);
    }
}