using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.States;
using Manager;
using Other.Dep;
using Other;

namespace Animation.Player.Controller
{
    /// <summary>
    /// PlayerController class for managing player states and animations
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerAnimatorController _anim;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Transform _spriteTransform;

        [Header("Config")]
        [SerializeField] private PlayerSO _playerSO;

        [Header("Raycast check ground")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundCheckRadius = 0.2f;

        [Header("Dep")]
        [SerializeField] private DepSO _currentDepSO;
        [SerializeField] private Transform _depSpawnPoint;

        //public
        public float InputX => _moveDirection;
        public bool IsGrounded => Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        public PlayerAnimatorController Anim => _anim;
        public string PlayerName => _playerSO.Data.PlayerName;
        public float Speed => _playerSO.Data.Speed;
        public float JumpForce => _playerSO.Data.JumpForce;


        public Rigidbody2D Rigidbody => _rb;
        private bool _jumpPressed;
        public bool JumpPressed => _jumpPressed;

        //flip
        private bool _facingRight = true;

        private FloatingJoystick _joystick;
        private PlayerState _currentState;
        private float _moveDirection;
        private int _jumpCount = 0;
        private int _maxJumpCount = 1;
        private bool _wasGroundedLastFrame;
        private bool wasRunningLastFrame = false;

        //Cool down attack
        private float _attackCooldown = 0.5f;
        private float _lastAttackTime = 0f;

        private float _attack2Cooldown = 5f;
        private float _lastAttack2Time = 0f;


        private void Awake()
        {
            _joystick = GameManager.Instance.Joystick;
        }

        private void Start()
        {
            //Camera
            CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
            if (camFollow != null)
            {
                camFollow.SetTarget(transform);
            }

            //Load Data
            if (_playerSO != null)
            {
                _playerSO.LoadData();
                _maxJumpCount = _playerSO.Data.MaxJumpCount;
            }

            //Load vũ khí đã lưu
            string weaponName = PlayerPrefs.GetString("SelectedWeapon", "");
            if (!string.IsNullOrEmpty(weaponName))
            {
                WeaponSO savedWeapon = Resources.Load<WeaponSO>("SO/Shop/" + weaponName);
                if (savedWeapon != null && savedWeapon.Data.DepSO != null)
                {
                    SetDepSO(savedWeapon.Data.DepSO);
                    Debug.Log("Load vũ khí: " + weaponName);
                }
            }

            ChangeState(new IdleState(this));
        }

        private void Update()
        {
            HandleInput();
            _currentState?.Update();
            _anim.UpdateMovement(_moveDirection * _playerSO.Data.Speed, IsGrounded, _rb.velocity.y); // fix chạm ground của attack

            if (IsGrounded && !_wasGroundedLastFrame)
            {
                ResetJumpCount();
            }
            _wasGroundedLastFrame = IsGrounded;

            // Phát âm thanh run
            bool isRunningNow = IsGrounded && Mathf.Abs(_moveDirection) > 0;

            if (isRunningNow && !wasRunningLastFrame)
            {
                SoundManager.Instance.PlayRun();
            }

            wasRunningLastFrame = isRunningNow;
        }

        private void FixedUpdate()
        {
            MoveHorizontal();
        }

        private void HandleInput()
        {
            // Đọc input từ joystick
            float rawInput = _joystick.Horizontal;

            // Deadzone để tránh "trôi" khi joystick không ở chính giữa
            const float deadZone = 0.2f;
            if (Mathf.Abs(rawInput) < deadZone)
            {
                _moveDirection = 0f;
            }
            else
            {
                // Chỉ lấy hướng ±1 để di chuyển trái/phải nhất quán
                _moveDirection = Mathf.Sign(rawInput);
            }

            // Chỉ flip khi đang di chuyển
            if (_moveDirection != 0f)
            {
                FlipCharacter(_moveDirection);
            }
        }


        public void MoveHorizontal()
        {
            _rb.velocity = new Vector2(_moveDirection * _playerSO.Data.Speed, _rb.velocity.y);
        }

        public void Jump()
        {
            if (CanJump())
            {
                PerformJump();
            }
        }

        private void PerformJump()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, JumpForce);
            _jumpCount++;
            ChangeState(new InAirState(this));
            SoundManager.Instance.PlayJumpSound();
        }

        private bool CanJump()
        {
            return _jumpCount < _maxJumpCount;
        }

        private void ResetJumpCount()
        {
            _jumpCount = 0;
        }

        private void FlipCharacter(float direction)
        {
            bool shouldFaceRight = direction > 0;

            if (shouldFaceRight != _facingRight)
            {
                _facingRight = shouldFaceRight;
                Vector3 theScale = _spriteTransform.localScale;
                theScale.x *= -1;
                _spriteTransform.localScale = theScale;
            }
        }

        public void SetDepSO(DepSO newDepSO)
        {
            if (newDepSO == null)
            {
                Debug.LogWarning("DepSO mới bị null, không đổi được!");
                return;
            }
            _currentDepSO = newDepSO;
            Debug.Log("Đã đổi DepSO sang: " + newDepSO.name);
        }


        // private void FlipCharacter(float direction)
        // {
        //     _spriteTransform.localScale = new Vector3(direction > 0 ? 1 : -1, 1, 1);
        // }


        public void ChangeState(PlayerState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void SetJumpPressed() => _jumpPressed = true;
        public void ResetJumpPressed() => _jumpPressed = false;

        // Gọi từ input system hoặc button để test animation
        // public void TriggerAttack1() => ChangeState(new Attack1State(this));
        // public void TriggerAttack2() => ChangeState(new Attack2State(this));
        public void TriggerAttack1()
        {
            if (Time.time - _lastAttackTime < _attackCooldown) return;
            _lastAttackTime = Time.time;
            //if (!(_currentState is Attack1State))
            ChangeState(new Attack1State(this));
        }

        public void TriggerAttack2()
        {
            if (Time.time - _lastAttack2Time < _attack2Cooldown) return;
            _lastAttack2Time = Time.time;
            ChangeState(new Attack2State(this));
        }
        public void ThrowDep()
        {
            if (_currentDepSO == null || _currentDepSO.Data == null || _currentDepSO.Data.DepPrefab == null)
            {
                Debug.LogWarning("DepSO hoặc prefab dép chưa được gán!");
                return;
            }

            GameObject dep = Instantiate(
                _currentDepSO.Data.DepPrefab,
                _depSpawnPoint.position,
                Quaternion.identity
            );

            Vector2 direction = _facingRight ? Vector2.right : Vector2.left;
            dep.GetComponent<DepBullet>().SetDirection(direction);
        }



        // public void TriggerAttack2()
        // {
        //     if (Time.time - _lastAttackTime < _attackCooldown) return;
        //     _lastAttackTime = Time.time;
        //     //if (!(_currentState is Attack2State))
        //     ChangeState(new Attack2State(this));
        // }



        public void TriggerDeath()
        {
            Debug.Log("TriggerDeath() được gọi trong PlayerController");
            ChangeState(new DeathState(this));
        }

        public void TriggerHit() => ChangeState(new HitState(this));
        public void TriggerShowOff() => ChangeState(new ShowOffState(this));
        public void TriggerInjured() => ChangeState(new InjuredState(this));
        public void TriggerIdlePoisoned() => ChangeState(new IdlePoisonedState(this));
    }
}
