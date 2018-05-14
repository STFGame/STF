using System.Collections;
using UnityEngine;

namespace Character
{
    public enum JumpMode { None, Jump, Descend }

    /// <summary>
    /// Handles the jumping and falling of the character.
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
    public class CharacterJump : MonoBehaviour, IGroundable
    {
        #region Jump Variables
        [Header("Jump Values")]
        //How high the character can jump
        [SerializeField] [Range(0f, 20f)] private float jumpHeight = 5;

        //How fast the character descends on command
        [SerializeField] [Range(0f, 200f)] private float descendSpeed = 0f;

        //How much of a delay between the jump command and the character jumping
        [SerializeField] [Range(0f, 2f)] private float jumpDelay = 0f;

        //How many times the character can jump
        [SerializeField] [Range(0, 5)] private int maxJumps = 1;

        //[SerializeField] private JumpInfo jumpInfo = null;
        //The start grounded state of the character
        [SerializeField] private bool grounded = true;

        private ParticleSystem landParticle = null;

        //Components to be cached
        private new Rigidbody rigidbody;
        private new Transform transform;
        private Animator animator;
        private Gravity gravity;

        //Tracks the last height of the character to determine if the character is falling
        private float lastHeight = 0f;

        //A variable that is set to max jumps and decrements everytime the characters jumps
        //while the variable is greater than 0
        private int jumpCount;

        public bool Grounded { get; set; }
        public bool Falling { get; private set; }
        #endregion

        #region Load
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            transform = GetComponent<Transform>();
            animator = GetComponent<Animator>();
            gravity = GetComponent<Gravity>();

            lastHeight = rigidbody.position.y;
            jumpCount = maxJumps;
        }

        #endregion

        #region Updates
        //Makes the character jump for as long as the jumpHold is held
        public void Jump(bool jump, bool jumpHold, JumpMode jumpMode)
        {
            if (Grounded)
            {
                lastHeight = transform.position.y;
                jumpCount = maxJumps;
            }

            if (jumpMode == JumpMode.Descend)
                rigidbody.AddForce(Vector3.down * descendSpeed);

            if (jump && jumpCount > 0 && jumpMode != JumpMode.Descend)
            {
                float velocity = Mathf.Sqrt(2f * gravity.Gravitation * jumpHeight);
                StartCoroutine(JumpDelay(velocity));
            }

            if (gravity)
                gravity.DecreaseGravity(jumpHold);
        }

        //Coroutine for the delay of the jump
        private IEnumerator JumpDelay(float velocity)
        {
            if (!Grounded)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, velocity, 0f);
                jumpCount--;
                yield break;
            }

            yield return new WaitForSeconds(jumpDelay);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, velocity, 0f);

            yield return new WaitUntil(() => Grounded == false);
            jumpCount--;
        }

        //Checks to see whether the character is falling based on the comparision of their current height and their last height
        public void Fall(float currentHeight)
        {
            if (currentHeight >= lastHeight)
            {
                lastHeight = currentHeight;
                Falling = false;
                return;
            }

            lastHeight = currentHeight + 0.5f;
            Falling = true;
        }
        #endregion

        #region Visual FX and Animations
        public void LandParticles(GameObject particleEffect)
        {
            if (landParticle == null)
            {
                Instantiate(particleEffect, transform.position, transform.rotation, transform);
                landParticle = particleEffect.GetComponent<ParticleSystem>();
            }

            landParticle.transform.position = Vector3.zero;
            landParticle.gameObject.SetActive(false);
        }

        public void AnimateJump(bool jump)
        {
            animator.SetBool("Jump", jump);
            animator.SetBool("Falling", Falling);
            animator.SetBool("Grounded", Grounded);
            animator.SetInteger("Jump Count", jumpCount + 1);
        }
    }
    #endregion
}