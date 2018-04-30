using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public abstract class Animation
    {
        [SerializeField][Range(0f, 1f)] public float saveTimer = 0f;

        protected Animator animator;
        protected StateMachineBehaviour behaviour;

        protected float timer = 0f;

        public abstract void Init<T>(MonoBehaviour mono) where T : StateMachineBehaviour;
    }
}
