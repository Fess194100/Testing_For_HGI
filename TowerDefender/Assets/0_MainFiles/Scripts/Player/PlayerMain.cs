using System;
using UnityEngine;

namespace UltimatePlayer
{    public class PlayerMain : MonoBehaviour
    {
        public Animator animator;
        [Space(5)]
        public PlayerData playerData;
        public PlayerState playerState;

        [Header("Possibilities")]
        public bool CanMoved = true;

        private PlayerInputSystem playerInputSystem;
        private CharacterController characterController;
        private Camera PlayerCamera;
        private float currentSpeed;
        private float currentGravity;
        private float turnSmoothVelocity;
        private Vector3 moveInput;
        private Vector3 desiredMove;

        private const float TimeTurnSmooth = 0.1f;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            PlayerCamera = Camera.main;
            playerInputSystem = new PlayerInputSystem();
            playerInputSystem.Enable();
        }

        private void OnEnable()
        {
            InterfaceManager.OnCanMovedChanged += CanMovedChenged;
        }

        private void OnDisable()
        {
            InterfaceManager.OnCanMovedChanged -= CanMovedChenged;
        }

        private void CanMovedChenged(object sender, CanMovedPlayerEvent newValue)
        {
            CanMoved = newValue.canMovedPlayer;
        }

        void FixedUpdate()
        {
            if (CanMoved)
            {
                GetMoveInput();
                GetSpeedMoved();
                GetGravity();

                desiredMove = moveInput * currentSpeed;
                desiredMove.y = -currentGravity;
                Vector3 Moved = desiredMove * Time.fixedDeltaTime;
                characterController.Move(Moved);
            }
        }

        private void GetMoveInput()
        {
            Vector3 currenrMoveInput = Vector3.zero;

            if (moveInput != Vector3.zero) { currenrMoveInput = moveInput; }

            moveInput = playerInputSystem.Gameplay.Moved.ReadValue<Vector2>();
            moveInput.z = moveInput.y;
            moveInput.y = 0f;
            GetStatePlayer();

            moveInput = Quaternion.Euler(0, PlayerCamera.transform.eulerAngles.y, 0) * moveInput;

            if (moveInput != Vector3.zero)
            {
                float targetRotation = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg;
                float smoothedRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, TimeTurnSmooth);
                transform.rotation = Quaternion.Euler(0, smoothedRotation, 0);
            }
            else
            {
                moveInput = currenrMoveInput * Mathf.Lerp(1, 0, 3 * Time.fixedDeltaTime);
            }
        }
        private void GetStatePlayer()
        {
            playerState.isGraunded = characterController.isGrounded;

            if (moveInput != Vector3.zero) { playerState.isMoved = true; }
            else { playerState.isMoved = false; }

            if (animator != null)
            {
                animator.SetBool("Moved", playerState.isMoved);
                animator.SetBool("Grounded", characterController.isGrounded);
            }
        }
        private void GetSpeedMoved()
        {
            if (playerState.isMoved)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, playerData.Walking.MaxSpeedWalk, playerData.Walking.AccelerationWalkingCurve.Evaluate(Time.fixedDeltaTime));
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, playerData.Walking.BrakingWalkingCurve.Evaluate(Time.fixedDeltaTime));
            }
        }

        private void GetGravity()
        {
            if (playerState.isGraunded)
            { 
                currentGravity = playerData.Physics.Gravity;
            }
            else
            {
                currentGravity += (playerData.Physics.Gravity - playerData.Physics.Resistance ) * Time.fixedDeltaTime;
            }
        }
    }
}
