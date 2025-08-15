using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class UIHealthBar : UIDialog
    {
        public static UIHealthBar Instance { get; private set; }

        [Header("Element UI")]
        [SerializeField] private Image _targetIcon;
        [SerializeField] private TMP_Text _targetNameText;
        [SerializeField] private Slider _healthBarSlider;

        private CanvasGroup _canvasGroup;

        protected override void Awake()
        {
            Instance = this;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Hide();
        }

        public void SetTarget(Sprite icon, string name, int maxHealth, int currentHealth)
        {
            _targetIcon.sprite = icon;
            _targetNameText.text = name;
            _healthBarSlider.maxValue = maxHealth;
            _healthBarSlider.value = currentHealth;
            Show();
        }

        public void UpdateHealth(int currentHealth)
        {
            _healthBarSlider.value = currentHealth;
            if (currentHealth <= 0) Hide();
        }

        public override void Show()
        {
            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 1f;
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            }
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
