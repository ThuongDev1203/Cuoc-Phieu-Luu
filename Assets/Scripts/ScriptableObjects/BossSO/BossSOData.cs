using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.BossSO
{
    [Serializable]
    public class BossSOData
    {
        [SerializeField] private string _bossName;
        [SerializeField] private float _chaseRange;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _health;

        public string BossName => _bossName;
        public float ChaseRange => _chaseRange;
        public float MoveSpeed => _moveSpeed;
        public int MaxHealth => _maxHealth;
        public int Health => _health;
    }
}

