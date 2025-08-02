using System;
using UnityEngine;

namespace ScriptableObjects.TrapSO
{
    [Serializable]
    public class TrapDataSO
    {
        [SerializeField] private string trapName;
        [SerializeField] private float damage;

        public string TrapName => trapName;
        public float Damage => damage;
    }
}
