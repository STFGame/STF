using Survival;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CubeAttack : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        IDamagable[] objectHit = other.GetComponentsInParent<IDamagable>();
        if (objectHit != null)
            for (int i = 0; i < objectHit.Length; i++)
                objectHit[i].TakeDamage(damage);
    }
}
