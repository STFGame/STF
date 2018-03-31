using Character.Instrument;
using Character.Instrument.Profiles;
using Character.Utility;
using System.Collections;
using UnityEngine;

namespace Character.CC
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody rigidBody;
        private Animator animator;
        private MovementBehaviour movementBehaviour;
        private Animation anim;

        [Header("Movement Settings")]
        [SerializeField] private float baseSpeed = 200f;
        [SerializeField] private float dashSpeed = 400f;

        [Header("Animation Settings")]
        [SerializeField] private float smoothDamping = 0.02f;
        [SerializeField] private float rotationSpeed = 10f;

        private float horizontal;
        private float vertical;

        private bool turn = false;
        private bool dashVal = false;
        private float rotation = 90f;

        private void OnEnable() { GetComponent<Instru>().AxleUnit += Move; }

        private void OnDisable() { GetComponent<Instru>().AxleUnit -= Move; }

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

        private void Move(Vector2 value)
        {
            Vector3 move = new Vector3(value.x, 0f, 0f);
            rigidBody.AddForce((move * baseSpeed) * Time.deltaTime, ForceMode.Impulse);

            QuaternionRotation(value);
            MoveAnimations(value);
            Turn(value);
        }

        private void Turn(Vector2 value)
        {
            turn = (rigidBody.transform.forward.x * value.x < 0f);
        }

        private void QuaternionRotation(Vector2 value)
        {
            rotation = (value.x < 0f) ? 270 : (value.x > 0f) ? 90f : rotation;

            Quaternion angleAxis = Quaternion.AngleAxis(rotation, Vector3.up);
            Quaternion rotate = Quaternion.Slerp(rigidBody.rotation, angleAxis, Time.deltaTime * rotationSpeed);
            rigidBody.rotation = (rotate);
        }

        private void Update()
        {
            //horizontal = Input.GetAxis("Horizontal");
            //vertical = Input.GetAxis("Vertical");

            //turn = (rigidBody.transform.forward.x * horizontal) < 0f;
            //dashVal = Input.GetKey(KeyCode.Joystick1Button3);

            //rotation = (horizontal < 0f) ? -90 : (horizontal > 0f) ? 90 : rotation;

            //Quaternion angleAxis = Quaternion.AngleAxis(rotation, Vector3.up);
            //Quaternion rotate = Quaternion.Slerp(rigidBody.rotation, angleAxis, Time.deltaTime * 10.0f);
            //rigidBody.rotation = (rotate);

            //MoveAnimations();
        }

        private void FixedUpdate()
        {
            //float speed = (dashVal) ? dashSpeed : baseSpeed;
            //Vector3 move = new Vector3(horizontal, 0f, 0f);
            //rigidBody.AddForce((move * speed) * Time.deltaTime, ForceMode.Impulse);
        }

        public void Rotate(int s)
        {
            Debug.Log("In rotate: " + rotation);
        }

        private void MoveAnimations(Vector2 value)
        {
            //animator.SetBool("Turn", turn);
            //animator.SetBool("Dash", dashVal);
            animator.SetFloat("XAxis", Mathf.Abs(value.x), smoothDamping, Time.deltaTime);
            animator.SetFloat("YAxis", value.y, smoothDamping, Time.deltaTime);
        }
    }
}
