using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Other
{
    public class DiamondTrigger : MonoBehaviour
    {
        [SerializeField] private int diamondValue = 1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                SoundManager.Instance.PlayCoinColect();
                GameManager.Instance.diamondManager.AddDiamond(diamondValue);
                Destroy(gameObject);
            }
        }
    }
}
