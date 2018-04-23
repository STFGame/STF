using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public class MovementAnimation : Animation
    {
        [SerializeField] private string moveName = "Speed";
        [SerializeField] private string crouchName = "Vertical";
        [SerializeField] private string dashName = "IsDashing";
        [SerializeField] private string turnName;
        [SerializeField] private string dashTurnName;

        [SerializeField] [Range(0f, 1f)] private float moveSmoothing = 0.1f;
        [SerializeField] [Range(0f, 1f)] private float crouchSmoothing = 0.1f;

        public override void Init<T>(MonoBehaviour mono)
        {
            animator = mono.GetComponent<Animator>();
            behaviour = animator.GetBehaviour<T>();
        }

        public void Move(float speed)
        {
            float animSpeed = Mathf.Abs(speed);
            animator.SetFloat(moveName, animSpeed, moveSmoothing, Time.deltaTime);
        }

        public void Crouch(float crouch){ animator.SetFloat(crouchName, crouch, crouchSmoothing, Time.deltaTime); }

    }
}
