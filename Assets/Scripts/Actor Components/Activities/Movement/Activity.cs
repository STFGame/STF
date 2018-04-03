using Controller.Mechanism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Activities
{
    [Serializable]
    public abstract class Activity
    {
        [SerializeField] protected float speed;
        [SerializeField] protected float rotationSpeed;

        protected IControl control;

        protected Animator animator;
        protected Transform transform;

        protected bool isTurning = false;

        public Activity() { }

        public virtual void Initialize(Component component, IControl control)
        {
            this.control = control;

            animator = component.GetComponent<Animator>();
            transform = component.GetComponent<Transform>();
        }

        public virtual void OnUpdate() { isTurning = HasTurned(transform.forward.x, control.Lever.Horizontal); }

        public virtual void OnFixedUpdate() { }

        protected virtual bool HasTurned(float forward, float input) { return (forward * input < 0f); }

        #region Properties
        public virtual float Speed
        {
            get { return speed; }
        }

        public virtual float RotationSpeed
        {
            get { return rotationSpeed; }
        }
        #endregion
    }
}
