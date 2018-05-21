using Survival;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHealth : MonoBehaviour, IDamagable
{
    public float currentHealth = 1000f;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
