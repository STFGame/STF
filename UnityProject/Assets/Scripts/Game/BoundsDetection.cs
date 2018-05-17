using Survival;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.collider.GetComponent<Health>();
        if (health != null)
            health.CurrentHealth = 0;
    }
}
