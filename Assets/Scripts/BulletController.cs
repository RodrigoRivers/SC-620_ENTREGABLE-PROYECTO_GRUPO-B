using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float damage = 50.0F;

    void OnTriggerEnter2D(Collider2D other)
    {
        HealthController controller = other.GetComponent<HealthController>();
        if (controller != null)
        {
            controller.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
