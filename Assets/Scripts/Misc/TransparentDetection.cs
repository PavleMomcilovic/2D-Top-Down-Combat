using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;

    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;
    private float originalTransparency;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
        originalTransparency = spriteRenderer ? spriteRenderer.color.a : tilemap.color.a;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>()) {
            if (spriteRenderer)
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, originalTransparency, transparencyAmount));
            if (tilemap)
                StartCoroutine(FadeRoutine(tilemap, fadeTime, originalTransparency, transparencyAmount));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>()) {
            if (spriteRenderer)
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, transparencyAmount,originalTransparency));
            if (tilemap)
                StartCoroutine(FadeRoutine(tilemap, fadeTime, transparencyAmount,originalTransparency));
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}
