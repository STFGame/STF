using Actor.Movements;
using Controls;
using UnityEngine;

namespace Actor
{
    public enum MotionState { Idle, Crouch, Walk, Run, Dash, Jump, Descend }

    /// <summary>ActorControl controls the Actor by passing in different values to the movement component.</summary>
    [RequireComponent(typeof(ActorMovement), typeof(ActorCombat), typeof(ActorSurvival))]
    [RequireComponent(typeof(ActorAnimation))]
    public class ActorControl : MonoBehaviour
    {
        public PlayerNumber playerNumber;

        private ActorMovement movement;
        private ActorCombat combat;
        private ActorSurvival survival;
        private new ActorAnimation animation;

        [HideInInspector] public Device device;

        [Header("Movement Controls")]
        [SerializeField] private MotionControl dashControl = new MotionControl();
        [SerializeField] private MotionControl crouchControl = new MotionControl();
        [SerializeField] private MotionControl descendControl = new MotionControl();

        private MotionState motionState = MotionState.Idle;

        private void Awake()
        {
            movement = GetComponent<ActorMovement>();
            combat = GetComponent<ActorCombat>();
            survival = GetComponent<ActorSurvival>();
            animation = GetComponent<ActorAnimation>();

            string name = Input.GetJoystickNames()[(int)playerNumber - 1];

            device = new Device(name, (int)playerNumber);
        }

        private void Update()
        {
            device.UpdateDevice();

            combat.Perform();

            UpdateMotionState();
        }

        private void FixedUpdate()
        {
            Vector3 move = new Vector3(device.LeftStick.Horizontal, device.LeftStick.Vertical, 0f);

            if (combat.AttackNumber <= 0)
                movement.Perform(move, device.RightBumper.Trigger, device.RightBumper.Hold);

        }

        private void UpdateMotionState()
        {
            if (dashControl.IsSuccessful(device.LeftStick.Horizontal))
                motionState = MotionState.Dash;
            else if (crouchControl.IsSuccessful(device.LeftStick.Vertical))
                motionState = MotionState.Crouch;
            else if (descendControl.IsSuccessful(device.LeftStick.Vertical))
                motionState = MotionState.Descend;
        }

        private void OnDrawGizmos() { }
    }
}
