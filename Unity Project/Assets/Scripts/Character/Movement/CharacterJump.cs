using System.Collections;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
    public class CharacterJump : MonoBehaviour
    {
        [SerializeField] private JumpInfo jumpInfo = null;

        private ParticleSystem landParticle = null;

        private new Rigidbody rigidbody;
        private Animator animator;
        private Gravity gravity;

        private float lastHeight = 0f;
        private int jumpCount;
        private bool grounded = true;

        public bool Falling { get; private set; }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            gravity = GetComponent<Gravity>();

            lastHeight = rigidbody.position.y;
            jumpCount = jumpInfo.maxJumps;
        }

        #region Ground Subscription
        private void OnEnable()
        {
            GetComponentInChildren<GroundCheck>().GroundEvent += Grounded;
        }

        private void OnDisable()
        {
            GetComponentInChildren<GroundCheck>().GroundEvent -= Grounded;
        }

        private void Grounded(bool grounded)
        {
            this.grounded = grounded;
        }
        #endregion

        public void Jump(bool jump, bool jumpHold)
        {
            if (grounded)
            {
                lastHeight = transform.position.y;
                jumpCount = jumpInfo.maxJumps;
            }

            if (jump && jumpCount > 0)
            {
                float velocity = Mathf.Sqrt(2f * gravity.Gravitation * jumpInfo.jumpHeight);
                StartCoroutine(JumpDelay(velocity));
            }

            if (gravity != null)
                gravity.DecreaseGravity(jumpHold);
        }

        private IEnumerator JumpDelay(float velocity)
        {
            if (!grounded)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, velocity, 0f);
                jumpCount--;
                yield break;
            }

            yield return new WaitForSeconds(jumpInfo.jumpDelay);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, velocity, 0f);

            yield return new WaitUntil(() => grounded == false);
            jumpCount--;
        }

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

        #region VFX
        public void LandParticles(GameObject particleEffect)
        {
            if (landParticle == null)
            {
                Instantiate(particleEffect, transform.position, transform.rotation, transform);
                landParticle = particleEffect.GetComponent<ParticleSystem>();
            }
            
            landParticle.transform.position = Vector3.zero;
            landParticle.gameObject.SetActive(false);
            //landParticle.gameObject.SetActive(true);
            //landParticle.Simulate(0f, true, true);
            //landParticle.Clear();
            //landParticle.Play();
            Debug.Log("Hello!");
        }

        public void AnimateJump(bool jump)
        {
            animator.SetBool("Jump", jump);
            animator.SetBool("Falling", Falling);
            animator.SetBool("Grounded", grounded);
            animator.SetInteger("Jump Count", jumpCount + 1);
        }
    }
    #endregion
}