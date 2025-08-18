using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    [DisallowMultipleComponent]
    public class UIHealthBar : UIDialog
    {
        public static UIHealthBar Instance { get; private set; }

        [Header("Element UI")]
        [SerializeField] private Image _targetIcon;
        [SerializeField] private TMP_Text _targetNameText;
        [SerializeField] private TMP_Text _targetHealthText;
        [SerializeField] private Slider _healthBarSlider;

        [Header("Refs")]
        [SerializeField] private CanvasGroup _canvasGroup; // cho phép drag thả trong Inspector

        protected override void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            // Tìm CanvasGroup theo thứ tự: self -> children -> auto add
            if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null) _canvasGroup = GetComponentInChildren<CanvasGroup>(true);
            if (_canvasGroup == null) _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        private void Start()
        {
            Hide();
        }

        public void SetTarget(Sprite icon, string name, int maxHealth, int currentHealth)
        {
            if (_targetIcon) _targetIcon.sprite = icon;
            if (_targetNameText) _targetNameText.text = name;

            if (_healthBarSlider)
            {
                _healthBarSlider.maxValue = maxHealth;
                _healthBarSlider.value = currentHealth;
            }

            if (_targetHealthText)
                _targetHealthText.text = $"{currentHealth}/{maxHealth}";

            Show();
        }

        public void UpdateHealth(int currentHealth)
        {
            if (_healthBarSlider)
                _healthBarSlider.value = currentHealth;

            if (_targetHealthText)
                _targetHealthText.text = $"{currentHealth}/{(int)_healthBarSlider.maxValue}";

            if (currentHealth <= 0) Hide();
        }

        public override void Show()
        {
            // Ép hiện
            if (!gameObject.activeSelf) gameObject.SetActive(true);

            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 1f;
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            }

            //lên trên cùng Canvas
            transform.SetAsLastSibling();
        }

        public override void Hide()
        {
            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 0f;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            }
        }
    }
}
