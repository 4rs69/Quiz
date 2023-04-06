using System;
using DG.Tweening;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{

    public class LevelCompletedScreen : BaseScreen
    {
        [Header("References")] [SerializeField]
        private Image _winImage;

        [SerializeField] private RectTransform _restartButton;

        [SerializeField] private RectTransform _nextLevelButton;

        [SerializeField] private RectTransform _finalCongratulationsPanel;

        [SerializeField] private RectTransform _betweenLevelsCongratulationsPanels;

        [Space] [Header("Animation settings")] [SerializeField]
        private float _durationBounceAnimation = 2f;

        [SerializeField] private float _durationWinImageAnimation = 2f;

        private IDisposable _subscription;

        private void Awake()
        {
            _subscription = EventStreams.Game.Subscribe<LevelCompletedEvent>(OnLevelCompleted);
        }

        private void OnLevelCompleted(LevelCompletedEvent eventData)
        {
            _winImage.rectTransform.localScale = Vector3.zero;
            _winImage.sprite = eventData.Cell.Image.sprite;
            _winImage.rectTransform.DOScale(Vector3.one, _durationWinImageAnimation).SetEase(Ease.OutBounce);
        }

        public void ShowRestartView()
        {
            _nextLevelButton.localScale = Vector3.zero;
            _betweenLevelsCongratulationsPanels.localScale = Vector3.zero;
            PlayOutBounceAnimation(Vector3.one, _durationBounceAnimation,
                _finalCongratulationsPanel, _restartButton);
        }

        public void ShowNextLevelButton()
        {
            _finalCongratulationsPanel.localScale = Vector3.zero;
            _restartButton.localScale = Vector3.zero;
            PlayOutBounceAnimation(Vector3.one, _durationBounceAnimation,
                _nextLevelButton, _betweenLevelsCongratulationsPanels);
        }

        private void PlayOutBounceAnimation(Vector3 scale, float duration, params RectTransform[] components)
        {
            foreach (var rectTransform in components)
            {
                rectTransform.localScale = Vector3.zero;
                rectTransform.DOScale(scale, duration).SetEase(Ease.OutBounce);
            }
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}
