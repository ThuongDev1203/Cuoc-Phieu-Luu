using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.States;
using Manager;

namespace Animation.Player.Controller
{
    /// <summary>
    /// PlayerController class for managing player states and animations
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerAnimatorController anim;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform spriteTransform;

        [Header("Config")]
        [SerializeField] private PlayerSO playerSO;

        [Header("Raycast check ground")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckRadius = 0.2f;


        public Rigidbody2D Rigidbody => rb;

        private bool jumpPressed;
        public bool JumpPressed => jumpPressed;

        private FloatingJoystick joystick;
        private PlayerState currentState;
        private float moveDirection;

        private void Awake()
        {
            joystick = GameManager.Instance.Joystick;
        }

        private void Start()
        {
            if (playerSO != null)
            {
                playerSO.LoadData();
            }
            ChangeState(new IdleState(this));
        }

        private void Update()
        {
            HandleInput();
            currentState?.Update();
        }

        private void FixedUpdate()
        {
            MoveHorizontal();
        }

        private void HandleInput()
        {
            moveDirection = joystick.Horizontal;

            if (Mathf.Abs(moveDirection) > 0.1f)
            {
                FlipCharacter(moveDirection);
            }
        }

        public void MoveHorizontal()
        {
            rb.velocity = new Vector2(moveDirection * playerSO.Data.speed, rb.velocity.y);
        }

        public void Jump()
        {
            if (IsGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            }
        }



        private void FlipCharacter(float direction)
        {
            spriteTransform.localScale = new Vector3(direction > 0 ? 1 : -1, 1, 1);
        }

        public float InputX => moveDirection;
        public bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        public PlayerAnimatorController Anim => anim;
        public string PlayerName => playerSO.Data.playerName;
        public float Speed => playerSO.Data.speed;
        public float JumpForce => playerSO.Data.jumpForce;

        public void ChangeState(PlayerState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        public void SetJumpPressed() => jumpPressed = true;
        public void ResetJumpPressed() => jumpPressed = false;

        // Gọi từ input system hoặc button để test animation


        public void TriggerAttack1() => ChangeState(new Attack1State(this));
        public void TriggerAttack2() => ChangeState(new Attack2State(this));
        public void TriggerDeath() => ChangeState(new DeathState(this));
        public void TriggerHit() => ChangeState(new HitState(this));
        public void TriggerShowOff() => ChangeState(new ShowOffState(this));
        public void TriggerInjured() => ChangeState(new InjuredState(this));
        public void TriggerIdlePoisoned() => ChangeState(new IdlePoisonedState(this));
    }
}
