using Actor.Bubbles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor
{
    [Serializable]
    public sealed class Attack
    {
        public enum AttackType
        {
            None,
            LightAttack1, LightAttack2, LightAttack3,
            HeavyAttack1, HeavyAttack2, HeavyAttack3,

            UpAttack1, UpAttack2, UpAttack3, UpAttack4,
            DownAttack1, DownAttack2, DownAttack3, DownAttack4,

            HorizontalAttack1, HorizontalAttack2, HorizontalAttack3, HorizontalAttack4,

            Count
        }

        [SerializeField] [Range(0f, 1000f)] private float attackDamage = 10f;
        [SerializeField] Vector2 displacement = Vector2.zero;
        [SerializeField] Vector2 attackForce = Vector2.zero;
        [SerializeField] AttackType attackType;
    
    }
}
