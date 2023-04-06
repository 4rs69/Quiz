using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Events;
using GameData;
using Pools;
using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    public event Action<Cell> OnClicked;

    [SerializeField] private RectTransform _cellsSpawnParent;

    [SerializeField] private Cell _cellPrefab;

    private MonoBehaviourPool<Cell> _pool;
    private IDisposable _subscription;

    private void Start()
    {
        _pool = new MonoBehaviourPool<Cell>(_cellPrefab, _cellsSpawnParent);
        _subscription = EventStreams.Game.Subscribe<HideCellsRequest>(HidePreviousLevelCells);
    }

    public void Spawn(IReadOnlyList<GameItem> selectedGameSetItems)
    {
        foreach (var gameItem in selectedGameSetItems)
        {
            var cell = _pool.Take();
            cell.SetClickCallBack(value => OnClicked?.Invoke(value));
            cell.Initialize(gameItem.View);
        }

        PlayAnimation(_pool.UsedItems.ToList());
    }

    private void HidePreviousLevelCells(HideCellsRequest eventData)
    {
        _pool.ReleaseAll();
    }

    private void PlayAnimation(List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            cell.transform.localScale = Vector3.zero;
            cell.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
        }
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}