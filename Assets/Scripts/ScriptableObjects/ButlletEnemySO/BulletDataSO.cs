using System;
using UnityEngine;

namespace ScriptableObjects.BulletEnemy
{
    [Serializable]
    public class BulletDataSO
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifetime;
        [SerializeField] private GameObject _prefab;

        public int Damage => _damage;
        public float Speed => _speed;
        public float Lifetime => _lifetime;
        public GameObject Prefab => _prefab;
    }
}

