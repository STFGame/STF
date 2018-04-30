using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public class HitAnimation : Animation
    {
        [SerializeField] private string hitName = "HitIndex";

        public override void Init<T>(MonoBehaviour mono)
        {
            animator = mono.GetComponent<Animator>();
        }

        public void PlayHit(int hitIndex) { animator.SetInteger(hitName, hitIndex); }
    }
}
