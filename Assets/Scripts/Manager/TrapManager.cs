using System.Collections;
using System.Collections.Generic;
using Animation.Player.Controller;
using Animation.Player.States;
using UnityEngine;

namespace Manager
{
    /// <summary>
    /// TrapManager class for managing traps in the game
    /// </summary>
    public class TrapManager : MonoBehaviour
    {
        [Header("Trap Settings")]
        private float _hitInterval = 0.5f;

        [Header("Config")]
        [SerializeField] private TrapSO trapSO;

        private bool isPlayerInTrap = false;
        private Coroutine hitCoroutine = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && hitCoroutine == null)
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                if (player != null)
                {
                    isPlayerInTrap = true;
                    hitCoroutine = StartCoroutine(HitLoop(player));
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isPlayerInTrap = false;

                if (hitCoroutine != null)
                {
                    StopCoroutine(hitCoroutine);
                    hitCoroutine = null;
                }
            }
        }

        IEnumerator HitLoop(PlayerController player)
        {
            while (isPlayerInTrap)
            {
                player.ChangeState(new HitState(player));
                yield return new WaitForSeconds(_hitInterval);
            }
        }
    }
}


