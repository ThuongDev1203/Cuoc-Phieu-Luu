using System;
using UnityEngine;

namespace SriptableObjects
{
    /// <summary>
    /// PlayerDataSO class for storing player data as a ScriptableObject
    /// </summary>
    [Serializable]
    public class TrapDataSO
    {
        public string trapName;
        public float damage = 10f;
    }
}
