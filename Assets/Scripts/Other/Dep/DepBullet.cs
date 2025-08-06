using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects.DepDataSO;

namespace Other.Dep
{
    public class DepBullet : MonoBehaviour
    {
        [Header("DepSO")]
        [SerializeField] private DepSO depSO;

        private Vector2 _direction = Vector2.right;

        void Start()
        {
            // Tự hủy sau thời gian tồn tại
            Destroy(gameObject, depSO.Data.DepLifetime);
        }

        void Update()
        {
            // Di chuyển theo hướng
            transform.Translate(_direction * depSO.Data.DepSpeed * Time.deltaTime, Space.World);
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;

            // Xoay theo hướng bay
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Debug.Log($"Hit {collision.name} with {depSO.Data.DepName} dealing {depSO.Data.DepDamage} damage.");
                Destroy(gameObject);
            }
        }
    }
}
