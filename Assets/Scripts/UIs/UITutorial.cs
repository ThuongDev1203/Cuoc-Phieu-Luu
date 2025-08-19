using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs
{
    public class UITutorial : UIPanel
    {

        public override void Show()
        {
            Debug.Log("Đang mở UI Tutorial");
            base.Show();
        }

        private void Update()
        {
            if (gameObject.activeSelf && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
            {
                Hide();
            }
        }
    }
}
