using Actor.Bubbles;
using Actor.Movements;
using Actor.StateBehaviours;
using Controls;
using UnityEngine;
using Utility;

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
        private ControlManager comboManager;

        private MasterBehaviour masterBehaviour;

        [HideInInspector] public Device device = null;

        [Header("Gravity Controls")]
        [SerializeField] private Gravity gravity = new Gravity();

        [Header("Movement Controls")]
        [SerializeField] private AxisControl dashControl = new AxisControl();
        [SerializeField] private AxisControl crouchControl = new AxisControl();
        [SerializeField] private AxisControl descendControl = new AxisControl();

        private StateMachine currentState = StateMachine.Idle;
        private Vector3 joystickDirection = Vector3.zero;

        #region Initialization
        private void Awake()
        {
            actorMovement = GetComponent<ActorMovement>();
            actorCombat = GetComponent<ActorCombat>();
            actorSurvival = GetComponent<ActorSurvival>();
            comboManager = GetComponentInChildren<ControlManager>();

            if (playerNumber != PlayerNumber.None)
            {
                string name = Input.GetJoystickNames()[(int)playerNumber - 1];
                device = new Device(name, (int)playerNumber);
            }
        }
        #endregion

        private void Update()
        {
            if (device == null)
                return;

            device.UpdateDevice();
            comboManager.UpdateControl(device);

            joystickDirection = new Vector3(device.LeftStick.Horizontal, device.LeftStick.Vertical, 0f);

            Combat();
            Survival(joystickDirection);

            ControlMovement();

        }

        private void FixedUpdate()
        {
            if (device == null)
                return;

            Move(joystickDirection);
        }

        private void Survival(Vector3 move)
        {
            bool block = device.RightBumper.Hold;

            actorSurvival.PerformSurvival(block);

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

            if (CanMove())
            {
                actorMovement.Move(move);
                actorMovement.PlayMovementAnimations(move);
            }
            else
                actorMovement.PlayMovementAnimations(Vector3.zero);

            if (CanJump())
            {
                actorMovement.Jump(move.y, jumpCommand, jumpHold, ref gravity);
                actorMovement.PlayJumpAnimations(move, jumpCommand);
            }

            if (CanRotate())
                actorMovement.Rotate(move, actorSurvival.IsBlocking);

            device.LeftBumper.Trigger = false;
        }

        private bool CanJump()
        {
            bool canJump = true;

            if (currentState == StateMachine.Stun)
                canJump = false;

            return canJump;
        }

        private bool CanRotate()
        {
            bool canRotate = true;

            if (currentState == StateMachine.Stun)
                canRotate = false;

            return canRotate;
        }

        private bool CanMove()
        {
            bool canMove = true;

            if (currentState == StateMachine.Block || currentState == StateMachine.Dodge ||
                currentState == StateMachine.Roll || currentState == StateMachine.Stun || actorCombat.IsAttacking)
                canMove = false;

            return canMove;
        }

        private void ControlMovement()
        {
            actorMovement.IsDashing = dashControl.IsSuccessful(device.LeftStick.Horizontal);
            actorMovement.IsCrouching = crouchControl.IsSuccessful(device.LeftStick.Vertical);
            actorMovement.IsTurning = device.LeftStick.Horizontal * transform.forward.x < 0f;
            actorMovement.IsFastFalling = descendControl.IsSuccessful(device.LeftStick.Vertical) && !actorMovement.OnGround;
        }
        #endregion

        private void UpdateState(StateMachine updateState)
        {
            currentState = updateState;
        }
    }
}
