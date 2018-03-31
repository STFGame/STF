using Entity.Animation;
using Entity.Components;
using Utility.Enums;
using System;
using UnityEngine;
using Controller.Mechanism;

/* JUMP PROPERTIES
 * Sean Ryan
 * March 17, 2018
 * 
 * This class is responsible for the jump properties of a entity.
 * These properties include animation parameters as well as other jump parameters.
 */

namespace Entity.Bounces
{
    [Serializable]
    public class Leap
    {
        [SerializeField] private Vivacity ebullience;

        [SerializeField] private float jumpHeightTap;
        [SerializeField] private float jumpHeightHold;
        private float jumpHeight;

        [SerializeField] private float jumpDelay;

        private float crest;

        private bool crestReached = false;
        private bool onGround = true;

        private IControl console;
        private IUnit unit;

        public void Initialize(IControl console, IUnit unit)
        {
            this.console = console;
            this.unit = unit;

            ebullience.Initialize(unit.Animator);
            crest = unit.Transform.position.y;
        }

        public void OnUpdate(bool onGround)
        {
            this.onGround = onGround;
            crestReached = IsCrestedReached(unit.Transform.position.y);

            PlayEbullience();
            jumpHeight = SetJumpHeight(jumpHeight);
        }

        private float SetJumpHeight(float jumpHeight)
        {
            float previousHeight = jumpHeight;
            if (console.GetButton(ButtonType.Action1).ActionHold)
                return jumpHeight = (console.GetButton(ButtonType.Action1).HoldTimer < 0.1f) ? jumpHeightTap : jumpHeightHold;
            return previousHeight;
        }

        private bool IsCrestedReached(float currentHeight)
        {
            if (onGround)
                return false;

            if (currentHeight >= crest)
            {
                crest = currentHeight;
                return false;
            }
            return true;
        }

        private void PlayEbullience()
        {
            //ebullience.Play<bool>(onGround);
        }

        #region Properties
        public bool CrestReached
        {
            get { return crestReached; }
        }

        public float JumpHeight
        {
            get { return jumpHeight; }
        }

        public float JumpDelay
        {
            get { return jumpDelay; }
        }

        public float Crest
        {
            get { return crest; }
            set { crest = value; }
        }
        #endregion
    }
}
