using Character.Collisions;
using UnityEngine;

namespace Character.CC
{
    public class Jump : MonoBehaviour
    {
        private Rigidbody rigidBody;
        private CapsuleCollider capsuleCollider;
        private Animator animator;

        [Header(header: "Jump Settings", order = 1)]

        [SerializeField] private float jumpHeight = 30f;
        [SerializeField] private int numberOfJumps = 2;
        [SerializeField] private float downPushFactor = 200f;

        public bool isGrounded = true;
        private bool jumped = false;
        private bool fall = false;
        private float crest;

        private float downPush = 0f;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            capsuleCollider = GetComponent<CapsuleCollider>();

            GetComponent<Collides>().OnGrounded += Grounded;
        }

        private void Update()
        {
            ApplyForceDown();

            jumped = (Input.GetKeyDown(KeyCode.Joystick1Button1));
            fall = CrestReached(rigidBody.position.y);
            JumpAnimations();
        }

        private void ApplyForceDown()
        {
            if (!isGrounded)
            {
                if(downPush < downPushFactor)
                    downPush += Time.deltaTime * downPushFactor;
                rigidBody.AddForce(Vector3.down * downPush);

                return;
            }

            downPush = 0f;
        }

        private void JumpVelocity(int jumpStart)
        {
            if (jumpStart == 1)
                rigidBody.velocity = Vector3.up * jumpHeight;
        }

        private void JumpAnimations()
        {
            animator.SetBool("Jump", jumped);
            animator.SetBool("Fall", fall);
            animator.SetBool("IsGrounded", isGrounded);
        }

        private bool CrestReached(float currentHeight)
        {
            if (isGrounded)
            {
                crest = currentHeight;
                return false;
            }
            else if (currentHeight >= crest)
            {
                crest = currentHeight;
                return false;
            }
            else
                return true;
        }

        private void Grounded(bool val) { isGrounded = val; }
    }
}
