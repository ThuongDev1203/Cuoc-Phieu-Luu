using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UIs
{
    public class UIDialog : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        private Vector3 originalScale;

        protected virtual void Awake()
        {
            originalScale = canvasGroup.transform.localScale;
        }

        /// <summary>
        /// Show dialog with scale-in and fade-in effect
        /// </summary>
        public virtual void Show()
        {
            ShowWithEffect();
        }

        /// <summary>
        /// Hide dialog with scale-out and fade-out effect
        /// </summary>
        public virtual void Hide()
        {
            HideWithEffect();
        }

        /// <summary>
        /// Show dialog with effect
        /// </summary>
        public void ShowWithEffect()
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
            canvasGroup.alpha = 0;
            canvasGroup.transform.localScale = Vector3.zero;

            canvasGroup.DOFade(1f, 0.3f);
            canvasGroup.transform.DOScale(originalScale, 0.3f).SetEase(Ease.OutBack);
        }

        /// <summary>
        /// Hide dialog with effect
        /// </summary>
        public void HideWithEffect()
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            canvasGroup.DOFade(0, 0.2f);
            canvasGroup.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        }
    }
}

