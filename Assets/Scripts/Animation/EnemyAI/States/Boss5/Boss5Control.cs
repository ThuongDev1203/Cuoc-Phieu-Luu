
using Animation.State.Boss.Run;
using UnityEngine;
namespace Annimation.Boss.Manager
{
    public class Boss5Control : MonoBehaviour
    {
        public Transform PlayerTransform;
        public BossStateMachine stateMachine;
        public Animator _animator;
        public LayerMask layerMask;
        [SerializeField] private EnemyAISO _bossData;

        [Header("Raycast check ground")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundCheckRadius = 0.2f;

        [Header("Raycast Circle")]
        [SerializeField] private float _detectionRadius;
        private float walkSpeed => _bossData.Data.Speed;
        private Rigidbody2D _rigidbody;
        private IdleBoss5 _idleState;
        private RunBoss5 _runState;

        public BossStateMachine _stateMachine { get; private set; }

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            stateMachine = new BossStateMachine();
            _idleState = new IdleBoss5(this, stateMachine);
            _runState = new RunBoss5(this, stateMachine);
            stateMachine.AddState(_idleState);
            stateMachine.AddState(_runState);
            if (_animator == null)
            { 
                Debug.LogError("Animator component is missing on the Boss5Control GameObject.");
            }
           idle();


        }
        private bool BossCheck()
        {
            Collider2D hit = Physics2D.OverlapCircle(_groundCheck.position, _detectionRadius, layerMask);
            if (hit != null)
            {
                if (hit.CompareTag("Player")) 
                {
                    Debug.Log("Boss detected the player within range.");
                    return true;
                }
            }
            return false;
        }

        void Update()
        {
            if (IsGrounded)
            {
                // Nếu đang idle và thấy player thì chuyển sang run
                if (stateMachine.CurrentState is IdleBoss5 && BossCheck())
                {
                    RunBoss();
                }

                // Nếu đang run thì mới gọi MoveTowards
                if (stateMachine.CurrentState is RunBoss5)
                {
                    MoveTowards();
                }
            }

            stateMachine?.Update();
        }



        public bool IsGrounded => Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        public void Flip(Vector2 direction)
        {
            if (direction.x != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = direction.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
        }
        public void MoveTowards()
        {
                if (PlayerTransform != null && walkSpeed != 0)
                {
                    Vector2 targetPosition = PlayerTransform.position;
                    Vector2 direction = targetPosition - (Vector2)transform.position;
                    Flip(direction);
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, walkSpeed * Time.deltaTime);
                }
        }

    public void RunBoss() => stateMachine.ChangeState(stateMachine.GetState<RunBoss5>());
    public void idle() => stateMachine.ChangeState(_idleState);

    }
}