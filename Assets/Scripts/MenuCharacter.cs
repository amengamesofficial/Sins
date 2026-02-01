using UnityEngine;
using System.Collections;

public class MenuCharacter : MonoBehaviour
{
    public Animator animator;

    [Header("Animation Settings")]
    public int animationCount = 3;

    public int minRepeat = 1;
    public int maxRepeat = 4;

    public float animationDuration = 1f; // ??? ?????? ?? ???????

    int currentAnim = -1;

    void Start()
    {
        StartCoroutine(AnimationLoop());
    }

    IEnumerator AnimationLoop()
    {
        while (true)
        {
            int nextAnim = GetRandomAnimation();

            int repeatCount = Random.Range(minRepeat, maxRepeat + 1);

            for (int i = 0; i < repeatCount; i++)
            {
                animator.SetInteger("AnimIndex", nextAnim);
                yield return new WaitForSeconds(animationDuration);
            }

            currentAnim = nextAnim;
        }
    }

    int GetRandomAnimation()
    {
        int rand;

        do
        {
            rand = Random.Range(0, animationCount);
        }
        while (rand == currentAnim);

        return rand;
    }
}
