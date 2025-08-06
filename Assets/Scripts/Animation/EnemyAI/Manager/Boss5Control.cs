using Animation.Boss5State.States;
using Animation.State.DeathBoss;
using Animation.State.Boss.Hit;
using Animation.State.Boss.Run;
using Animation.State.Boss.Attack;
using Animation.State.Boss.Idle;
using UnityEngine;
namespace Annimation.Boss.Manager
{
    public class Boss5Control : MonoBehaviour
    {

        public Transform GetCurrentTarget() => _currentTarget;
        public BossStateMachine stateMachine;
        public Animator _animator;
        public LayerMask layerMask;
        [SerializeField] private EnemyAISO _bossData;
        [SerializeField] private Transform _targetA;
        [SerializeField] private Transform _targetB;

        [Header("Raycast check ground")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundCheckRadius = 0.2f;

        [Header("Raycast Circle")]
        [SerializeField] private float _detectionRadius = 5f;
        private float detectedRange => _bossData.Data.DetectRange;
        private float walkSpeed => _bossData.Data.Speed;
        private Transform _currentTarget;
        private Rigidbody2D _rigidbody;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            stateMachine = new BossStateMachine();
            if (_animator != null)
            {
                stateMachine.AddState(new IdleBossState(this, stateMachine));
                stateMachine.AddState(new RunBossState(this, stateMachine));
                stateMachine.AddState(new HitbossState(this, stateMachine));
                stateMachine.AddState(new DeathBossState(this, stateMachine));
                stateMachine.AddState(new AttackBossState(this, stateMachine));
                stateMachine.Initialize(new IdleBossState(this, stateMachine));
            }
        }
        private void BossCheck()
        {
            Collider2D hit = Physics2D.OverlapCircle(_groundCheck.position, _detectionRadius, layerMask);
            if (hit != null)
            {
                _currentTarget = hit.transform;
            }
            else
            {
                _currentTarget = _targetA; 
            }
        }
       void Update()
        {
            if (IsGrounded)
            {
                BossCheck();

                if (!(stateMachine.CurrentState is RunBossState))
                {
                    stateMachine.ChangeState(stateMachine.GetState<RunBossState>());
                }
            }

            stateMachine?.Update();
        }

        public bool IsGrounded => Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        public void SwitchTarget()
        {
            _currentTarget = _currentTarget == _targetA ? _targetB : _targetA;
        }
        public void Flip(Vector2 direction)
        {
            if (direction.x != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = direction.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
        }
         public void MoveTowards(Vector2 target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, walkSpeed * Time.deltaTime);
        }
    }
}