using System;
using UnityEngine;

namespace Actor.Animation
{
    /// <summary>
    /// A class that is responsible for the movement animations of the Actor
    /// </summary>
    [Serializable]
    public class MovementAnimation
    {
        [SerializeField] private string movementName = "Speed";
        [SerializeField] private string dashName = "HasDashed";
        [SerializeField] private float smoothing = 0.2f;

        private MovementBehaviour movementBehaviour;
        private Animator animator;

        public void Init(Movement movement)
        {
            animator = movement.GetComponent<Animator>();
            movementBehaviour = animator.GetBehaviour<MovementBehaviour>();

            if (movementBehaviour != null)
                movementBehaviour.movement = movement;
        }

        public void SetSpeed(float value) { animator.SetFloat(movementName, value, smoothing, Time.deltaTime); }

        public void SetDash(bool value) { animator.SetBool(dashName, value); }
    }
}
