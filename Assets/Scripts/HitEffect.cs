using System.Collections;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [Header("Target Sprite")]
    [SerializeField] private SpriteRenderer targetSprite;

    [Header("Timing")]
    [SerializeField] private float grayDuration = 0.1f;

    private Color originalColor;
    private Coroutine effectCoroutine;

    private void Awake()
    {
        if (targetSprite == null)
            targetSprite = GetComponent<SpriteRenderer>();

        originalColor = targetSprite.color;
    }

    private void OnEnable()
    {
        // ??? ????? ?? ??? ???? ????? ???? ??
        if (effectCoroutine != null)
            StopCoroutine(effectCoroutine);

        effectCoroutine = StartCoroutine(GrayEffect());
    }

    private IEnumerator GrayEffect()
    {
        // ????? ?? ???????
        targetSprite.color = Color.gray;

        yield return new WaitForSeconds(grayDuration);

        // ?????? ?? ??? ????
        targetSprite.color = originalColor;

        // ??? ???? ??? ???????
        gameObject.SetActive(false);
    }
}
