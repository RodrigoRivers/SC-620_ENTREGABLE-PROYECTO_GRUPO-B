using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour
{
    public float lifeLossAmount = 100.0F;

    void OnTriggerEnter2D(Collider2D other)
    {
            HealthController healthController = other.GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.TakeDamage(lifeLossAmount);
            }
    }
}
