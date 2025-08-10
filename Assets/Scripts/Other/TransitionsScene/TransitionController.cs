using UnityEngine;
using System;
using DG.Tweening;

namespace Manager
{
    public class TransitionController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CanvasGroup transitionCanvas;
        [SerializeField] private RectTransform iconTransform;

        [Header("Settings")]
        public float fullScale = 3f; // scale cuối cùng khi zoom to
        public float transitionTime = 2f;

        private void Awake()
        {
            if (transitionCanvas != null)
            {
                transitionCanvas.alpha = 255f;
                transitionCanvas.blocksRaycasts = false;
                transitionCanvas.interactable = false;
            }

            if (iconTransform != null)
            {
                iconTransform.localScale = Vector3.zero;  // bắt đầu nhỏ
            }
        }

        public void ShowTransition(Action onComplete = null)
        {
            if (transitionCanvas == null)
            {
                Debug.LogWarning("TransitionCanvas chưa được gán!");
                onComplete?.Invoke();
                return;
            }

            // Bật raycast trong lúc chuyển để chặn tương tác UI bên dưới
            transitionCanvas.blocksRaycasts = true;
            transitionCanvas.interactable = true;

            Sequence seq = DOTween.Sequence();

            // Chỉ zoom icon lên fullScale
            if (iconTransform != null)
            {
                seq.Append(iconTransform.DOScale(Vector3.one * fullScale, transitionTime * 0.5f).SetEase(Ease.OutBack));
            }
            else
            {
                // Nếu icon không có thì vẫn giữ thời gian delay để đồng bộ
                seq.AppendInterval(transitionTime * 0.5f);
            }

            // Callback khi zoom to xong
            seq.AppendCallback(() =>
            {
                onComplete?.Invoke();
            });

            // Zoom icon về 0
            if (iconTransform != null)
            {
                seq.Append(iconTransform.DOScale(Vector3.zero, transitionTime * 0.5f).SetEase(Ease.InBack));
            }
            else
            {
                seq.AppendInterval(transitionTime * 0.5f);
            }

            // Tắt raycast sau khi kết thúc
            seq.AppendCallback(() =>
            {
                transitionCanvas.blocksRaycasts = false;
                transitionCanvas.interactable = false;
            });

            seq.Play();
        }
    }
}
