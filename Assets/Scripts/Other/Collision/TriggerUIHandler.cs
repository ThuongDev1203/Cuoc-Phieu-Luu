using UIs;
using UnityEngine;

namespace Other.Collision
{
    public class TriggerUIHandler : MonoBehaviour
    {
        private TutorialGameUI _uiToShow;

        void Start()
        {
            _uiToShow = FindObjectOfType<TutorialGameUI>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (_uiToShow != null)
                {
                    _uiToShow.Show();
                }
            }
        }
    }
}