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
        [SerializeField] public float knockBack;
        [SerializeField][Range(0f, 2f)] public float stunLength;

        public AttackDamage(float damage, float knockBack, float stunLength)
        {
            this.damage = damage;
            this.knockBack = knockBack;
            this.stunLength = stunLength;

            this.stunLength = Mathf.Clamp(stunLength, 0f, 2f);
        }
    }
}
