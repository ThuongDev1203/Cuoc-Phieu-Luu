using UnityEngine;

namespace Manager
{
    public class CoinManager : MonoBehaviour
    {
        private int totalCoins;          // Tổng coin đã lưu (tất cả level)
        private int coinsThisLevel;      // Coin kiếm được trong level hiện tại

        private const string COIN_KEY = "TotalCoins";

        public int TotalCoins => totalCoins;
        public int CoinsThisLevel => coinsThisLevel;

        private void Start()
        {
            // Load coin tổng từ PlayerPrefs
            totalCoins = PlayerPrefs.GetInt(COIN_KEY, 0);
            coinsThisLevel = 0;
        }

        /// <summary>
        /// Cộng coin vào level hiện tại (khi nhặt coin)
        /// </summary>
        public void AddCoinThisLevel(int amount)
        {
            coinsThisLevel += amount;
            GameManager.Instance.UpdateCoinUI(coinsThisLevel); // UI hiển thị coin level hiện tại
        }

        /// <summary>
        /// Khi thắng level, cộng coin của level vào tổng và lưu
        /// </summary>
        public void ApplyLevelCoins()
        {
            totalCoins += coinsThisLevel;
            SaveTotalCoins();
            GameManager.Instance.UpdateCoinUI(totalCoins); // UI hiển thị coin tổng
            coinsThisLevel = 0;
        }

        /// <summary>
        /// Trừ coin từ tổng (dùng khi mua vật phẩm)
        /// </summary>
        public bool SpendCoin(int amount)
        {
            if (totalCoins >= amount)
            {
                totalCoins -= amount;
                SaveTotalCoins();
                GameManager.Instance.UpdateCoinUI(totalCoins);
                return true;
            }

            Debug.LogWarning("Không đủ xu");
            return false;
        }

        /// <summary>
        /// Lấy tổng coin sau khi cộng thêm phần thưởng (chỉ tính tạm, không lưu)
        /// </summary>
        public int GetTotalCoinsAfterLevel(int rewardCoins)
        {
            return totalCoins + rewardCoins;
        }

        private void SaveTotalCoins()
        {
            PlayerPrefs.SetInt(COIN_KEY, totalCoins);
            PlayerPrefs.Save();
        }
    }
}
