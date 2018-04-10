using Actor.Animation;
using System.Collections;
using UnityEngine;

namespace Actor
{
    /// <summary>
    /// A component that handles the different jump aspects of the Actor.
    /// </summary>
    public class ActorJump : MonoBehaviour
    {
        [SerializeField] private float gravity = 10f;
        [SerializeField] private float jumpHeight = 20f;
        [SerializeField] private float jumpDelay = 0.1f;
        [SerializeField] private int jumpNumber = 2;

        private float crestHeight = 0f;
        private int jumpNumberReset;

        private new Rigidbody rigidbody;
        private new Transform transform;
        [SerializeField] private new JumpAnimation animation;

        public bool OnGround { get; private set; }
        public bool OnCrest { get; private set; }
        public float JumpCounter { get; set; }
        public float JumpDelay { get { return jumpDelay; } }
        public float JumpHeight { get { return jumpHeight; } }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            transform = GetComponent<Transform>();

            animation.Init(this);

            crestHeight = transform.localPosition.y;
            jumpNumberReset = jumpNumber;

            OnGround = true;

            GetComponent<ActorBubble>().OnGround += Ground_Event;
        }

        public void Jump(bool click)
        {
            OnCrest = CrestCheck(transform.localPosition.y);

            print("Jump");

            if (!OnGround)
                rigidbody.AddForce(Vector3.down * gravity);

            if (click)
            {
                StopAllCoroutines();
                StartCoroutine(Initiate(jumpDelay));
            }

            if (OnGround)
                ResetJump();

            Animation(click);
        }

        public void SetJumpHeight(bool hold)
        {
            if (hold && JumpCounter < jumpHeight)
                JumpCounter += 5f;

        }

        private IEnumerator Initiate(float delay)
        {
            if (!OnGround && jumpNumber <= 0)
                yield break;

            yield return new WaitForSeconds(jumpDelay);
            rigidbody.velocity = Vector2.up * JumpCounter;

            jumpNumber--;
            JumpCounter = 0f;
        }

        private bool CrestCheck(float currentHeight)
        {
            if (currentHeight >= crestHeight || OnGround)
            {
                crestHeight = currentHeight;
                return false;
            }
            else
                return true;
        }

        private void Animation(bool value)
        {
            animation.SetJump(value);
            animation.SetFall(OnCrest);
            animation.SetLand(OnGround);
        }

        private void ResetJump()
        {
            crestHeight = transform.localPosition.y;
            jumpNumber = jumpNumberReset;
        }

        private void Ground_Event(bool value) { OnGround = value; }
    }
}
