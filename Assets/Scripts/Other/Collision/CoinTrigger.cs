using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Other
{
    public class CoinTrigger : MonoBehaviour
    {
        [SerializeField] private int coinValue = 10;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameManager.Instance.coinManager.AddCoin(coinValue);
                Destroy(gameObject);
            }
        }
    }
}