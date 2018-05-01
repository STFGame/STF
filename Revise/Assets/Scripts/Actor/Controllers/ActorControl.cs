using Actor.Movements;
using Combos;
using Controls;
using UnityEngine;

namespace Actor
{
    /// <summary>ActorControl controls the Actor by passing in different values to the movement component.</summary>
    [RequireComponent(typeof(ActorMovement), typeof(ActorCombat), typeof(ActorSurvival))]
    public class ActorControl : MonoBehaviour
    {
        public PlayerNumber playerNumber;

        private ActorMovement actorMovement;
        private ActorCombat actorCombat;
        private ActorSurvival actorSurvival;
        private ComboManager comboManager;

        [HideInInspector] public Device device = null;

        [Header("Movement Controls")]
        [SerializeField] private MotionControl dashControl = new MotionControl();
        [SerializeField] private MotionControl crouchControl = new MotionControl();
        [SerializeField] private MotionControl descendControl = new MotionControl();

        public MovementState MovementState { get; private set; }

        private Vector3 move = Vector3.zero;

        private void Awake()
        {
            actorMovement = GetComponent<ActorMovement>();
            actorCombat = GetComponent<ActorCombat>();
            actorSurvival = GetComponent<ActorSurvival>();
            comboManager = GetComponentInChildren<ComboManager>();

            MovementState = MovementState.Regular;

            if (playerNumber != PlayerNumber.None)
            {
                string name = Input.GetJoystickNames()[(int)playerNumber - 1];
                device = new Device(name, (int)playerNumber);
            }
        }

        private void Update()
        {
            if (device == null)
                return;

            move = new Vector3(device.LeftStick.Horizontal, device.LeftStick.Vertical, 0f);

            device.UpdateDevice();

            comboManager.PerformCombos(device);

            Combat();
            Survival(move);

            ControlMovement();
        }

        private void FixedUpdate()
        {
            if (device == null)
                return;

            Move(move);
        }

        private void Survival(Vector3 move)
        {
            bool block = device.RightBumper.Hold;

            actorSurvival.PerformSurvival(block);

            actorSurvival.Roll(move);

            actorSurvival.Dodge(move);

            actorSurvival.PlaySurvivalAnimation();
        }

        private void Combat()
        {
            actorCombat.PerformAttack();
            actorCombat.PlayAttackAnimation();
        }

        #region Movement
        private void Move(Vector3 move)
        {
            bool jumpCommand = device.LeftBumper.Trigger;
            bool jumpHold = device.LeftBumper.Hold;

            if (!StopMovement())
                actorMovement.Move(move);

            if (!StopJump())
                actorMovement.Jump(move.y, jumpCommand, jumpHold);

            actorMovement.Rotate(move.x, actorSurvival.IsBlocking);

            actorMovement.PlayMovementAnimations(move);
            actorMovement.PlayJumpAnimations(move, jumpCommand);

            device.LeftBumper.Trigger = false;
        }

        private bool StopJump()
        {
            return actorSurvival.IsStunned;
        }

        private bool StopMovement()
        {
            return actorSurvival.IsBlocking || actorSurvival.IsStunned;
        }

        private void ControlMovement()
        {
            MovementState state = MovementState.None;

            if (dashControl.IsSuccessful(device.LeftStick.Horizontal))
            {
                state = MovementState.Dash;
            }
            else if (crouchControl.IsSuccessful(device.LeftStick.Vertical))
            {
                state = MovementState.Crouch;
            }
            else if (descendControl.IsSuccessful(device.LeftStick.Vertical) && !actorMovement.onGround)
            {
                state = MovementState.Descend;
            }
            else if (device.LeftStick.Horizontal * transform.forward.x < 0f)
            {
                state = MovementState.Turn;
            }
            else
            {
                state = MovementState.Regular;
            }

            actorMovement.UpdateMovementState(state);
        }
        #endregion

        private void OnDrawGizmos() { }
    }
}
