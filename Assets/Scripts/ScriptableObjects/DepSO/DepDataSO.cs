using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.DepDataSO
{
    [Serializable]
    public class DepDataSO
    {
        [SerializeField] private string _depName;
        [SerializeField] private float _depDamage;

        public string DepName => _depName;
        public float DepDamage => _depDamage;
    }
}

