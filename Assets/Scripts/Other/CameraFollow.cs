using UnityEngine;

namespace Other
{
    /// <summary>
    /// CameraFollow class for following a target with smooth movement
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [Header("Target")]
        private Transform _target;
        private string _playerTag = "Player";

        [Header("Offset")]
        private Vector3 _offset = new Vector3(0f, 1.5f, -10f);

        [Header("Smooth")]
        private float _smoothTime = 0.2f;

        private Vector3 _velocity = Vector3.zero;

        private void Start()
        {
            if (_target == null)
                InvokeRepeating(nameof(TryFindPlayer), 0f, 0.3f);
        }

        void TryFindPlayer()
        {
            if (_target != null)
            {
                CancelInvoke(nameof(TryFindPlayer));
                return;
            }

            GameObject player = GameObject.FindGameObjectWithTag(_playerTag);
            if (player != null)
            {
                _target = player.transform;
                CancelInvoke(nameof(TryFindPlayer));
            }
        }

        private void LateUpdate()
        {
            if (_target == null) return;

            Vector3 targetPosition = _target.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        }
    }
}
