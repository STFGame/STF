using System;
using UnityEngine;

namespace Combat
{
    /// <summary>
    /// Class that contains info about damage, knockback, etc that an attack does.
    /// </summary>
    [Serializable]
    public class Damage
    {
        public float damage = 10f;
        [SerializeField] private Vector3 attackDirection;
        [SerializeField] private float knockback = 10f;

        public Vector3 GetAttackVelocity()
        {
            return attackDirection * knockback;
        }
    }
}
