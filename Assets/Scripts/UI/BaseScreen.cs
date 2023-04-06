using UnityEngine;

namespace UI
{
    public class BaseScreen : MonoBehaviour
    {
        [SerializeField] private FadeController _fadeController;

        public void Show()
        {
            _fadeController.FadeIn();
        }

        public void Hide()
        {
            _fadeController.FadeOut();
        }
    }
}