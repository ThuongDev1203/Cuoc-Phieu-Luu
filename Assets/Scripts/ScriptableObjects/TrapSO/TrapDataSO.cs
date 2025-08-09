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
        [SerializeField] private float fallDelay;         // Độ trễ trước khi rơi
        [SerializeField] private float activationDistance;  // Khoảng cách kích hoạt
        [SerializeField] private float destroyAfter;        // Thời gian tự hủy
        [SerializeField] private float gravityScale;      // Trọng lực khi rơi

        public string TrapName => trapName;
        public int Damage => damage;
        public float FallDelay => fallDelay;
        public float ActivationDistance => activationDistance;
        public float DestroyAfter => destroyAfter;
        public float GravityScale => gravityScale;
    }
}
