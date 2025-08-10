using UnityEngine;

namespace Manager
{
    public class DiamondManager : MonoBehaviour
    {
        private int totalDiamonds;
        private int diamondsThisLevel;

        private const string DIAMOND_KEY = "TotalDiamonds";

        public int TotalDiamond => totalDiamonds;
        public int DiamondsThisLevel => diamondsThisLevel;

        private void Start()
        {
            totalDiamonds = PlayerPrefs.GetInt(DIAMOND_KEY, 0);
            diamondsThisLevel = 0;
        }

        public void AddDiamondThisLevel(int amount)
        {
            diamondsThisLevel += amount;
            GameManager.Instance.UpdateDiamondUI(diamondsThisLevel);
        }

        public void ApplyLevelDiamonds()
        {
            totalDiamonds += diamondsThisLevel;
            PlayerPrefs.SetInt(DIAMOND_KEY, totalDiamonds);
            GameManager.Instance.UpdateDiamondUI(totalDiamonds);
            diamondsThisLevel = 0;
        }

        public bool SpendDiamond(int amount)
        {
            if (totalDiamonds >= amount)
            {
                totalDiamonds -= amount;
                PlayerPrefs.SetInt(DIAMOND_KEY, totalDiamonds);
                GameManager.Instance.UpdateDiamondUI(totalDiamonds);
                return true;
            }
            Debug.LogWarning("Không đủ kim cương");
            return false;
        }

        public int GetTotalDiamondsAfterLevel(int rewardDiamond)
        {
            return totalDiamonds + rewardDiamond;
        }
    }
}
