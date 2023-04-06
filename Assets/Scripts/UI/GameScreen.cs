using DG.Tweening;
using Events;
using SimpleEventBus.Disposables;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameScreen : BaseScreen
    {
        [Header("References")] [SerializeField]
        private TextMeshProUGUI _goalLabel;

        [Space] [Header("Animation settings")] [SerializeField]
        private float _disappearDuration;

        [SerializeField, Range(0.1f, 0.3f)] private float _upscaleDuration;

        [SerializeField] private float _waitingTimeBeforeHidingCells;

        [SerializeField] private float _rotationDegreesY;

        [SerializeField] private float _rotationDuration;

        private CompositeDisposable _subscriptions;

        public void OnTargetCellClicked(Cell cell)
        {
            var sequence = DOTween.Sequence();

            sequence.AppendCallback(() => cell.Button.interactable = false)
                .Append(cell.Image.rectTransform.DOScale(Vector3.zero, _disappearDuration).SetEase(Ease.InBounce))
                .AppendCallback(() => EventStreams.Game.Publish(new LevelCompletedEvent(cell)))
                .AppendInterval(_waitingTimeBeforeHidingCells)
                .AppendCallback(() => EventStreams.Game.Publish(new HideCellsRequest()))
                .Append(cell.Image.rectTransform.DOScale(Vector3.one, _upscaleDuration))
                .AppendCallback(() => cell.Button.interactable = true);

        }

        public void SetGoal(string goal)
        {
            _goalLabel.text = $"Find {goal}";
        }

        public void OnWrongCellClicked(Cell cell)
        {
            var sequence = DOTween.Sequence();

            sequence.AppendCallback(() => cell.Button.interactable = false)
                .Append(cell.Image.rectTransform.DORotate(new Vector3(0, _rotationDegreesY, 0), _rotationDuration)
                    .SetEase(Ease.InBounce)
                    .SetLoops(2, LoopType.Yoyo))
                .AppendCallback(() => cell.Button.interactable = true);
        }

        private void OnDestroy()
        {
            _subscriptions?.Dispose();
        }
    }
}