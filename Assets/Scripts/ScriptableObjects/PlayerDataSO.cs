using System;
using UnityEngine;

namespace SriptableObjects
{
    /// <summary>
    /// PlayerDataSO class for storing player data as a ScriptableObject
    /// </summary>
    [Serializable]
    public class PlayerDataSO
    {
        public string playerName;
        public float speed = 5f;
        public float jumpForce = 10f;
        public int maxJumpCount = 2;
    }
}
