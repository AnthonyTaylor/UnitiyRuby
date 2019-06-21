using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public ParticleSystem healthPickUp;
    public AudioClip collectedClip;

    /// <summary>
    /// When trigger is activated,
    /// If by Ruby,
    /// Increase health and play sound when not already at max health
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        if (controller != null)
        {
            if(controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                ParticleSystem pickUp = Instantiate(healthPickUp);
                pickUp.transform.position = transform.position;
                pickUp.GetComponent<ParticleSystem>().Play();
               
                Destroy(gameObject);

                controller.PlaySound(collectedClip);

            }

        }
    }
}
