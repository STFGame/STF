using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Combat
{
    [Serializable]
    public class AttackDamage
    {
        [SerializeField] public float damage;
        [SerializeField] public float force;
        [SerializeField][Range(0f, 2f)] public float stunLength;

        public AttackDamage(float damage, float force, float stunLength)
        {
            this.damage = damage;
            this.force = force;
            this.stunLength = stunLength;

            this.stunLength = Mathf.Clamp(stunLength, 0f, 2f);
        }
    }
}
