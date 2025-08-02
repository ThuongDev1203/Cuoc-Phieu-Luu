using System;
using UnityEngine;

namespace SriptableObjects.PlayerSO
{
    [Serializable]
    public class PlayerDataSO
    {
        [SerializeField] private string _playerName;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private int _maxJumpCount;
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _attack1Damage;
        [SerializeField] private int _attack2Damage;
        [SerializeField] private float _attackRange;

        public string PlayerName => _playerName;
        public float Speed => _speed;
        public float JumpForce => _jumpForce;
        public int MaxJumpCount => _maxJumpCount;
        public int Health => _health;
        public int MaxHealth => _maxHealth;
        public int Attack1Damage => _attack1Damage;
        public int Attack2Damage => _attack2Damage;
        public float AttackRange => _attackRange;
    }
}
