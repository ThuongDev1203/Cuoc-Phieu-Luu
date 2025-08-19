using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class TutorialGameUI : UIPanel
    {
        [SerializeField] private GameObject _panel1;
        [SerializeField] private GameObject _panel2;
        [SerializeField] private Button _leftBtn;
        [SerializeField] private Button _rightBtn;
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
            _panel1.SetActive(false);
            _panel2.SetActive(false);

            if (_leftBtn != null)
            {
                _leftBtn.onClick.AddListener(SwitchTo_Panel1);
            }
            if (_rightBtn != null)
            {
                _rightBtn.onClick.AddListener(SwitchTo_Panel2);
            }
            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(Hide);
            }
        }

        public override void Show()
        {
            base.Show();
            _panel1.SetActive(true);
            _panel2.SetActive(false);
        }

        public override void Hide()
        {
            base.Hide();
            _panel1.SetActive(false);
            _panel2.SetActive(false);
        }

        public void SwitchTo_Panel1()
        {
            _panel1.SetActive(true);
            _panel2.SetActive(false);
        }

        public void SwitchTo_Panel2()
        {
            _panel1.SetActive(false);
            _panel2.SetActive(true);
        }
    }
}