using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public GameObject damageCollider;
    private bool hasAppliedDamage = false; 

  
    public void StartAttack()
    {
        hasAppliedDamage = false; 
        damageCollider.SetActive(true);
        StartCoroutine(HideSword());
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (hasAppliedDamage) return; 

        if (gameObject.tag == "Player")
        {
            if (collision.gameObject.tag == "Hizi")
            {
                EnemyHealthBar.instance.Health.value -= 1;
                HiziController.instance.Hit();
                hasAppliedDamage = true; 
            }
            else if (collision.gameObject.tag == "Gheibat")
            {
                EnemyHealthBar.instance.Health.value -= 1;
                GheibatController.instance.Hit();
                hasAppliedDamage = true;
            }
            else if (collision.gameObject.tag == "BadSelf")
            {
                EnemyHealthBar.instance.Health.value -= 1;
                BadSelfController.instance.Hit();
                hasAppliedDamage = true;
            }
        }
    }

    IEnumerator HideSword()
    {
        yield return new WaitForSeconds(0.15f); 
        damageCollider.SetActive(false);
    }
}