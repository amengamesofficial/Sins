using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    public GameObject Player;
    public GameObject DamageCollider;
    public float AttackRange = 5;
    public float Distance;

    public void Damage()
    {
        if (PlayerMovement.instance.isDefend && PlayerMovement.instance.IsFacing(transform.position))
        {
            PlayerMovement.instance.GetKnockback(transform.position, 2);

            if (PlayerBreathBar.instance.BreathBar.value <= 0)
            {
                
                PlayerMovement.instance.BreakGuard();
                PlayerHealthBar.instance.Health.value -= 1;
                PlayerMovement.instance.Hit();

            }
            return;
        }

        Distance = Vector2.Distance(DamageCollider.transform.position, Player.transform.position);
        if (Distance < AttackRange)
        {
            PlayerMovement.instance.Hit();
            PlayerHealthBar.instance.Health.value -= 1;
           

        }
    }
}