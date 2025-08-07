using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Other.Dep;

namespace ScriptableObjects.DepDataSO
{
    [Serializable]
    public class DepDataSO
    {
        [SerializeField] private string _depName;
        [SerializeField] private int _depDamage;
        [SerializeField] private float _depSpeed;
        [SerializeField] private float _depLifetime;
        [SerializeField] private DepType _depType;
        [SerializeField] private GameObject _depPrefab;

        public string DepName => _depName;
        public int DepDamage => _depDamage;
        public float DepSpeed => _depSpeed;
        public float DepLifetime => _depLifetime;
        public DepType DepType => _depType;
        public GameObject DepPrefab => _depPrefab;
    }
}

