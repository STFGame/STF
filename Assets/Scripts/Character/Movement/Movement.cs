using Character.Utility;
using System.Collections;
using UnityEngine;

namespace Character.CC
{
    public class Movement : MonoBehaviour
    {
        [HideInInspector]public Rigidbody rigidBody;
        private Animator animator;
        private Animation anim;
        private MovementBehaviour movementBehaviour;

        [Header("Movement Settings")]
        [SerializeField] private float baseSpeed = 140;
        [SerializeField] private float dashSpeed = 180;

        [Header("Animation Settings")]
        [SerializeField] private float smoothDamping = 0.02f;

        private float horizontal;
        private float vertical;

        private bool turn = false;
        private bool dashVal = false;
        [HideInInspector]public float rotation = 0f;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            anim = GetComponent<Animation>();
        }

        private void Start()
        {
            movementBehaviour = animator.GetBehaviour<MovementBehaviour>();
            if (movementBehaviour != null)
                movementBehaviour.movement = this;
        }

        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            turn = (rigidBody.transform.forward.x * horizontal) < 0f;
            dashVal = Input.GetKey(KeyCode.Joystick1Button3);

            rotation = (horizontal < 0f) ? -90 : (horizontal > 0f) ? 90 : rotation;

            Quaternion targetRot = Quaternion.Euler(0f, rotation, 0f);
            Quaternion rotate = Quaternion.Slerp(rigidBody.rotation, targetRot, Time.deltaTime * 10.0f);
            rigidBody.transform.rotation = (rotate);

            MoveAnimations();
        }

        private void FixedUpdate()
        {
            float speed = (dashVal) ? dashSpeed : baseSpeed;
            Vector3 move = new Vector3(horizontal, 0f, 0f);
            rigidBody.AddForce((move * speed) * Time.deltaTime, ForceMode.Impulse);
        }

        public void Rotate(int s)
        {
            Debug.Log("In rotate: " + rotation);
        }

        private void MoveAnimations()
        {
            //animator.SetBool("Turn", turn);
            animator.SetBool("Dash", dashVal);
            animator.SetFloat("XAxis", Mathf.Abs(horizontal), smoothDamping, Time.deltaTime);
            animator.SetFloat("YAxis", vertical, smoothDamping, Time.deltaTime);
        }
    }
}
