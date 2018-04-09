using Actor.Animation;
using Controller.Mechanism;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    [RequireComponent(typeof(Animator))]
    public class ActorAttack : MonoBehaviour
    {
        public float damage;

        [SerializeField] private new AttackAnimation animation;

        // Use this for initialization
        void Awake()
        {
            animation.Init(this);
        }

        public bool IsAttacking { get; set; }
    }
}