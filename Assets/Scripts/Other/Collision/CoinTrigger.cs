using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Other
{
    public class CoinTrigger : MonoBehaviour
    {
        [SerializeField] private int coinValue = 5;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                SoundManager.Instance.PlayCoinColect();
                GameManager.Instance.coinManager.AddCoinThisLevel(coinValue);
                Destroy(gameObject);
            }
        }
    }
}