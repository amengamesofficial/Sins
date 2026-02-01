using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class iceDamage : MonoBehaviour
{
    public int damageAmount = 2;

    BoxCollider2D iceCollider;
    void Start()
    {
        iceCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(EnableIceCollider());
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            PlayerHealthBar.instance.Health.value -= damageAmount;
            PlayerMovement.instance.Hit();

        }
    }

    IEnumerator EnableIceCollider()
    {
        yield return new WaitForSeconds(0.7f);
        iceCollider.enabled = true;
    }
}