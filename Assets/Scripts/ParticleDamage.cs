using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            if(PlayerMovement.instance.isDefend)
            {
                PlayerBreathBar.instance.BreathBar.value -= 0.5f;
                return;
            }
            
            if (gameObject.tag == "Lightning")
                PlayerHealthBar.instance.Health.value -= 0.5f;
            else if (gameObject.tag == "Fire")
                PlayerHealthBar.instance.Health.value -= 2;

            PlayerMovement.instance.Hit();
            PlayerHealthBar.instance.Health.value -= 1f;
        }
    }
}