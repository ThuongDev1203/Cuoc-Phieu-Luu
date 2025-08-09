using System;
using UnityEngine;

namespace ScriptableObjects.TrapSO
{
    [Serializable]
    public class TrapDataSO
    {
        [Header("Thông tin cơ bản")]
        [SerializeField] private string trapName;
        [SerializeField] private int damage;

        [Header("Cấu hình rơi")]
        [SerializeField] private float fallDelay = 0.2f;         // Độ trễ trước khi rơi
        [SerializeField] private float activationDistance = 5f;  // Khoảng cách kích hoạt
        [SerializeField] private float destroyAfter = 2f;        // Thời gian tự hủy
        [SerializeField] private float gravityScale = 1.5f;      // Trọng lực khi rơi (2D)

        public string TrapName => trapName;
        public int Damage => damage;
        public float FallDelay => fallDelay;
        public float ActivationDistance => activationDistance;
        public float DestroyAfter => destroyAfter;
        public float GravityScale => gravityScale;
    }
}
