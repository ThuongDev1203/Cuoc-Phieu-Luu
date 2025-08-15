using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.BossSO
{
    [Serializable]
    public class BossSOData
    {
        [SerializeField] private Sprite _bossIcon;
        [SerializeField] private string _bossName;
        [SerializeField] private float _chaseRange;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _attackDamage;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _health;

        public Sprite BossIcon => _bossIcon;
        public string BossName => _bossName;
        public float ChaseRange => _chaseRange;
        public float MoveSpeed => _moveSpeed;
        public int AttackDamage => _attackDamage;
        public float AttackCooldown => _attackCooldown;
        public int MaxHealth => _maxHealth;
        public int Health => _health;
    }
}

