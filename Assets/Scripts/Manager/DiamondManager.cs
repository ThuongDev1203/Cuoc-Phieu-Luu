using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class DiamondManager : MonoBehaviour
    {
        private int totalDiamonds;
        private const string DIAMOND_KEY = "TotalDiamonds";

        public int TotalDiamond => totalDiamonds;

        private void Start()
        {
            //totalDiamonds = PlayerPrefs.GetInt(DIAMOND_KEY, 0);
            GameManager.Instance.UpdateDiamondUI(totalDiamonds);
        }

        public void AddDiamond(int amount)
        {
            totalDiamonds += amount;
            //PlayerPrefs.SetInt(DIAMOND_KEY, totalDiamonds);
            GameManager.Instance.UpdateDiamondUI(totalDiamonds);
        }

        public bool SpendDiamond(int amount)
        {
            if (totalDiamonds >= amount)
            {
                totalDiamonds -= amount;
                //PlayerPrefs.SetInt(DIAMOND_KEY, totalDiamonds);
                GameManager.Instance.UpdateDiamondUI(totalDiamonds);
                return true;
            }

            Debug.LogWarning("Không đủ coin");
            return false;
        }
    }
}
