using System.Collections;
using Utility.Enums;
using UnityEngine;
using Utility.Gravity;
using Actor.Jumps;

/* JUMP
 * Sean Ryan
 * March 17, 2018
 * 
 * This class is responsible for all jump actions associated with the character.
 * These actions include physical jumping things, as well as animations.
 */

namespace Actor
{
    #region Required Components
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Transform))]
    #endregion
    public class ActorJump : Actor
    {
        [SerializeField] private Gravity gravity = new Gravity();
        [SerializeField] private Jump jump = new Jump();

        private new Rigidbody rigidbody;

        //Awake is called when the script instance is being loaded.
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            jump.Initialize(component, control);
        }

        //Updates every frame
        private void Update()
        {
            jump.OnUpdate(onGround);

            if(control.GetButton(ButtonType.Action1).ActionClick)
            {
                StopAllCoroutines();
                StartCoroutine(Delay());
            }

            //PlayAnimations();
        }

        //FixedUpdate is called every fixed framerate frame.
        private void FixedUpdate()
        {
            gravity.ApplyGravity(rigidbody, onGround);
        }

        //Coroutine that is responsible for the initial delay of a jump.
        //This is designed to sync the animation with the physical movement
        private IEnumerator Delay()
        {
            if (!onGround)
                yield break;
            float delay = (onGround) ? jump.JumpDelay : 0f;

            yield return new WaitForSeconds(delay);

            rigidbody.velocity = Vector2.up * jump.JumpHeight;
            yield break;
        }

        //Method that is responsible for playing the different animations associated with jumping
        private void PlayAnimations()
        {
            //unit.Animator.SetBool("OnGround", onGround);
            //unit.Animator.SetBool("HasJumped", control.GetButton(ButtonType.Action1).ActionClick);
            //unit.Animator.SetBool("IsFalling", jump[jumpIndex].CrestReached);
        }
    }
}
