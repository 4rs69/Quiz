using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class FadeControllerBackUp : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Transform _start;

        [SerializeField] private Transform _end;

        [SerializeField] private CanvasGroup _canvasGroup;

        [Space] [Header("Animation settings")] [SerializeField]
        private float _duration = 1f;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void FadeIn()
        {
            _canvasGroup.alpha = 0;
            _rectTransform.transform.position = _start.position;
            _rectTransform.DOAnchorPos(_end.position, _duration);
            _canvasGroup.DOFade(1, _duration);
        }

        public void FadeOut()
        {
            _canvasGroup.alpha = 1;
            _rectTransform.transform.position = _end.position;
            _rectTransform.DOAnchorPos(_start.position, _duration);
            _canvasGroup.DOFade(0, _duration);
        }
    }
}