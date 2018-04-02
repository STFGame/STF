using Controller.Mechanism;
using Entity.Bounces;
using Entity.Components;
using System;
using System.Collections;
using Utility.Enums;
using System.Collections.Generic;
using UnityEngine;
using Utility.Gravity;
using Entity.Jumps;

/* JUMP
 * Sean Ryan
 * March 17, 2018
 * 
 * This class is responsible for all jump actions associated with the character.
 * These actions include physical jumping things, as well as animations.
 */

namespace Entity
{
    #region Required Components
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Transform))]
    #endregion
    public class EntityJump : Entity
    {
        [SerializeField] private Gravity gravity = new Gravity();
        [SerializeField] private Jump[] jump;
        private int jumpIndex = 0;
        //[SerializeField] private Leap[] leaps;
        //private int leapIndex = 0;

        //Awake is called when the script instance is being loaded.
        private new void Awake()
        {
            base.Awake();

            for (int i = 0; i < jump.Length; i++)
                jump[i].Initialize(control, unit);
            //for (int i = 0; i < leaps.Length; i++)
            //leaps[i].Initialize(control, unit);
        }

        //Updates every frame
        private void Update()
        {
            jump[jumpIndex].OnUpdate(onGround);
            //leaps[leapIndex].OnUpdate(onGround);

            if (control.GetButton(ButtonType.Action1).ActionClick)
            {
                StopAllCoroutines();
                StartCoroutine(Delay());
            }

            PlayAnimations();
        }

        //FixedUpdate is called every fixed framerate frame.
        private void FixedUpdate()
        {
            gravity.ApplyGravity(unit.Rigidbody, onGround);
        }

        //Coroutine that is responsible for the initial delay of a jump.
        //This is designed to sync the animation with the physical movement
        private IEnumerator Delay()
        {
            if (!onGround && jumpIndex + 1 == jump.Length)
                yield break;
            jumpIndex = (onGround) ? 0 : jumpIndex + 1;

            float delay = (onGround) ? jump[jumpIndex].JumpDelay : 0f;

            yield return new WaitForSeconds(delay);

            unit.Rigidbody.velocity = Vector3.up * jump[jumpIndex].JumpHeight;
        }

        //Method that is responsible for playing the different animations associated with jumping
        private void PlayAnimations()
        {
            unit.Animator.SetBool("OnGround", onGround);
            unit.Animator.SetBool("HasJumped", control.GetButton(ButtonType.Action1).ActionClick);
            unit.Animator.SetBool("IsFalling", jump[jumpIndex].CrestReached);
        }
    }
}
