using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public class HealDataSO
    {
        [SerializeField] private string _nameHeal;
        [SerializeField] private int _healing;

        public string NameHeal => _nameHeal;
        public int Healing => _healing;
    }
}
