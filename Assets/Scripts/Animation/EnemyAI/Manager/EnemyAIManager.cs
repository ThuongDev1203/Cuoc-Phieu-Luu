using System.Collections;
using UnityEngine;
using Animation.EnemyAI.States;

namespace Animation.EnemyAI.Manager
{
    public class EnemyAIManager : MonoBehaviour
    {
        public EnemyStateMachine StateMachine { get; private set; }

        [Header("Target A -> B")]
        [SerializeField] private Transform _targetA;
        [SerializeField] private Transform _targetB;

        [Header("Config")]
        [SerializeField] private EnemyAISO _enemyAISO;
        public float walkSpeed => _enemyAISO.Data.Speed;

        public Animator animator;

        private Transform _currentTarget;
        public Transform GetCurrentTarget() => _currentTarget;

        private void Awake()
        {
            StateMachine = gameObject.AddComponent<EnemyStateMachine>();
        }

        private void Start()
        {
            _currentTarget = _targetB;
            StateMachine.ChangeState(new EnemySpawnState(this));
        }

        public void MoveTowards(Vector2 target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, walkSpeed * Time.deltaTime);
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

        public void SwitchTarget()
        {
            _currentTarget = _currentTarget == _targetA ? _targetB : _targetA;
        }

        public bool IsCurrentAnimFinished()
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.normalizedTime >= 1f && !animator.IsInTransition(0);
        }

        public void SetAnimation(string animName)
        {
            animator.Play(animName);
        }

        public void Die()
        {
            StateMachine.ChangeState(new EnemyDeathState(this));
        }
    }
}
