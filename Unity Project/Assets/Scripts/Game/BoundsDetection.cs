using Survival;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IHealth health = collision.collider.GetComponent<IHealth>();
        if (health != null)
            health.CurrentHealth = 0;
    }
}
