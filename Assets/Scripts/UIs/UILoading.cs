using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace UIs
{
    public class UILoading : MonoBehaviour
    {
        [Header("Time (in seconds) before hiding the loading screen")]
        public float loadingDuration = 2f;

        [Header("UI Elements")]
        public CanvasGroup canvasGroup;
        public Slider loadingSlider;
        public TextMeshProUGUI loadingText;

        private void Start()
        {
            canvasGroup.alpha = 1f;
            loadingSlider.value = 0f;

            StartCoroutine(FillLoadingBar());
        }

        IEnumerator FillLoadingBar()
        {
            float timer = 0f;

            while (timer < loadingDuration)
            {
                timer += Time.deltaTime;
                float progress = Mathf.Clamp01(timer / loadingDuration);

                loadingSlider.value = progress;
                loadingText.text = $"Loading {(int)(progress * 100)}%";

                yield return null;
            }

            loadingSlider.value = 1f;
            loadingText.text = "Loading 100%";

            yield return new WaitForSeconds(0.2f);

            canvasGroup.DOFade(0f, 0.5f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}
