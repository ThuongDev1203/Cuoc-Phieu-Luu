using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animation.Player.Controller
{
    /// <summary>
    /// PlayerAnimationController class for managing player animations
    /// </summary>
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private readonly int speedHash = Animator.StringToHash("Speed");
        private readonly int isGroundedHash = Animator.StringToHash("IsGrounded");
        private readonly int verticalSpeedHash = Animator.StringToHash("VerticalSpeed");
        private readonly int attack1TriggerHash = Animator.StringToHash("Attack 1");
        private readonly int attack2TriggerHash = Animator.StringToHash("Attack 2");
        private readonly int hitTriggerHash = Animator.StringToHash("Hit");
        private readonly int deathTriggerHash = Animator.StringToHash("Death");
        private readonly int showOffTriggerHash = Animator.StringToHash("Show Off");
        private readonly int injuredTriggerHash = Animator.StringToHash("Injured");
        private readonly int idlePoisonedTriggerHash = Animator.StringToHash("Idle Poisoned");

        /// <summary>
        /// Cập nhật các tham số chuyển động để Animator tự động chuyển trạng thái
        /// </summary>
        public void UpdateMovement(float speed, bool isGrounded, float verticalSpeed)
        {
            animator.SetFloat(speedHash, Mathf.Abs(speed));
            animator.SetBool(isGroundedHash, isGrounded);
            animator.SetFloat(verticalSpeedHash, verticalSpeed);
        }

        public void TriggerAttack1() => animator.SetTrigger(attack1TriggerHash);
        public void TriggerAttack2() => animator.SetTrigger(attack2TriggerHash);
        public void TriggerHit() => animator.SetTrigger(hitTriggerHash);
        public void TriggerDeath() => animator.SetTrigger(deathTriggerHash);
        public void TriggerShowOff() => animator.SetTrigger(showOffTriggerHash);
        public void TriggerInjured() => animator.SetTrigger(injuredTriggerHash);
        public void TriggerIdlePoisoned() => animator.SetTrigger(idlePoisonedTriggerHash);

        /// <summary>
        /// Kiểm tra animation hiện tại đã kết thúc chưa (cho animation một lần như death, attack...)
        /// </summary>
        public bool IsAnimationFinished()
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.normalizedTime >= 1f && !animator.IsInTransition(0);
        }
    }
}

