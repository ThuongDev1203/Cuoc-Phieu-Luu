using UnityEngine;

namespace Manager
{
    public class CoinManager : MonoBehaviour
    {
        private int _totalCoins;
        private const string COIN_KEY = "TotalCoins";

        public int TotalCoins => _totalCoins;

        private void Start()
        {
            //_totalCoins = PlayerPrefs.GetInt(COIN_KEY, 0);
            GameManager.Instance.UpdateCoinUI(_totalCoins);
        }

        public void AddCoin(int amount)
        {
            _totalCoins += amount;
            //PlayerPrefs.SetInt(COIN_KEY, _totalCoins);
            GameManager.Instance.UpdateCoinUI(_totalCoins);
        }

        public bool SpendCoin(int amount)
        {
            if (_totalCoins >= amount)
            {
                _totalCoins -= amount;
                //PlayerPrefs.SetInt(COIN_KEY, _totalCoins);
                GameManager.Instance.UpdateCoinUI(_totalCoins);
                return true;
            }

            Debug.LogWarning("Không đủ coin");
            return false;
        }
    }
}
