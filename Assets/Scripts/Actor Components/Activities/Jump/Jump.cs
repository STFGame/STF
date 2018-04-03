using Controller.Mechanism;
using System;
using UnityEngine;
using Utility.Enums;

namespace Actor.Jumps
{
    [Serializable]
    public class Jump
    {
        [SerializeField] private float jumpHeightTap;
        [SerializeField] private float jumpHeightHold;
        [SerializeField] protected float jumpDelay;

        protected float jumpHeight;

        protected Transform transform;
        protected float crest;

        protected bool crestReached;
        protected bool onGround;

        protected IControl control;

        public virtual void Initialize(Component component, IControl control)
        {
            this.control = control;
            transform = component.GetComponent<Transform>();
        }

        public virtual void OnUpdate(bool onGround)
        {
            this.onGround = onGround;

            jumpHeight = SetJumpHeight(jumpHeight);
            crestReached = IsCrestReached(transform.position.y);
        }

        private float SetJumpHeight(float height)
        {
            float currentHeight = height;
            float holdThreshold = jumpDelay;

            if (control.GetButton(ButtonType.Action1).ActionHold)
                return (control.GetButton(ButtonType.Action1).HoldTimer < holdThreshold) ? jumpHeightTap : jumpHeightHold;
            return currentHeight;
        }

        private bool IsCrestReached(float currentHeight)
        {
            if (currentHeight >= crest || onGround)
            {
                crest = currentHeight;
                return false;
            }

            return true;
        }

        #region Properties
        public float JumpDelay
        {
            get { return jumpDelay; }
        }

        public bool CrestReached
        {
            get { return crestReached; }
        }

        public float Crest
        {
            get { return crest; }
        }

        public float JumpHeight
        {
            get { return jumpHeight; }
        }
        #endregion
    }
}
