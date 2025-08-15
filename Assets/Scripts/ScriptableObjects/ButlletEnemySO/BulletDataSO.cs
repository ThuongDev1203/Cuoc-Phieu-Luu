using System;
using UnityEngine;

namespace ScriptableObjects.BulletEnemy
{
    [Serializable]
    public class BulletDataSO
    {
        [SerializeField] private int _Damage;
        [SerializeField] private float _Speed;
        [SerializeField] private float _Lifetime;
        [SerializeField] private GameObject _Prefab;

        public int Damage => _Damage;
        public float Speed => _Speed;
        public float Lifetime => _Lifetime;
        public GameObject Prefab => _Prefab;
    }
}

