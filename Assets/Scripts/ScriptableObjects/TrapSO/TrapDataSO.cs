using System;
using UnityEngine;

namespace ScriptableObjects.TrapSO
{
    [Serializable]
    public class TrapDataSO
    {
        [SerializeField] private string trapName;
        [SerializeField] private int damage;

        public string TrapName => trapName;
        public int Damage => damage;
    }
}
