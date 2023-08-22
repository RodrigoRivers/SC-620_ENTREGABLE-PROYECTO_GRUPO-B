using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchController : MonoBehaviour
{
    [SerializeField]
    float damage = 25.0F;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 contactPoint = other.GetContact(0).normal;

            Character2DController character = other.gameObject.GetComponent<Character2DController>();
            if (character != null)
            {
                character.Rebound(contactPoint);
            }

            HealthController controller = other.gameObject.GetComponent<HealthController>();
            if (controller != null)
            {
                controller.TakeDamage(damage);
            }
        }
    }

}
