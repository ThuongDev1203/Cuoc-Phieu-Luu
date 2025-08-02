using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SriptableObjects.EnemyAISO
{
    /// <summary>
    /// EnemyDataSO class for storing enemy data
    /// </summary>
    [Serializable]
    public class EnemyDataAISO
    {
        [SerializeField] private string _enemyName;
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        [SerializeField] private float _attackDamage;
        [SerializeField] private float _detectRange;

        public string EnemyName => _enemyName;
        public float Health => _health;
        public float Speed => _speed;
        public float AttackDamage => _attackDamage;
        public float DetectRange => _detectRange;

    }
}
