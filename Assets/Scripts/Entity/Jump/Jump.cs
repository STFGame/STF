using Controller.Mechanism;
using Entity.Animation;
using Entity.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Enums;

namespace Entity.Jumps
{
    [Serializable]
    public class Jump
    {
        [SerializeField] protected Vivacity vivacity;

        [SerializeField] private float jumpHeightTap;
        [SerializeField] private float jumpHeightHold;
        [SerializeField] private float holdThreshold;

        [SerializeField] protected float jumpDelay;

        protected float jumpHeight;
        protected float crest;

        protected bool crestReached;
        protected bool onGround;

        protected IControl control;
        protected IUnit unit;

        public virtual void Initialize(IControl control, IUnit unit)
        {
            this.unit = unit;
            this.control = control;
        }

        public virtual void OnUpdate(bool onGround)
        {
            this.onGround = onGround;

            jumpHeight = SetJumpHeight(jumpHeight);
            crestReached = IsCrestReached(unit.Transform.position.y);
        }

        private float SetJumpHeight(float height)
        {
            float currentHeight = height;
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
