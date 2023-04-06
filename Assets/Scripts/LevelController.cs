using System;
using System.Collections.Generic;
using Events;
using GameData;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class LevelController : MonoBehaviour
{
    public event Action<Cell> TargetCellClicked;
    public event Action<Cell> WrongCellClicked;

    public event Action<string> GoalSelected;

    [Header("References")] [SerializeField]
    private DataProvider _dataProvider;

    private CellSpawner _cellSpawner;
    private GameItem _goalItem;
    private IReadOnlyList<GameItem> _selectedSetItems;
    private IReadOnlyList<GameItem> _currentLevelItems;
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _cellSpawner = GetComponent<CellSpawner>();

        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<SetSelectedEvent>(OnSetSelected),
            EventStreams.Game.Subscribe<NextLevelButtonClickedEvent>(OnNextLevelButtonClicked),
        };

        _cellSpawner.OnClicked += OnCellClicked;
    }

    public void StartLevel()
    {
        _cellSpawner.Spawn(_currentLevelItems);

        _goalItem = _currentLevelItems.GetRandomItem(_goalItem);
        GoalSelected?.Invoke(_goalItem.Name);
    }

    private void OnCellClicked(Cell cell)
    {
        if (cell.Image.sprite == _goalItem.View)
        {
            TargetCellClicked?.Invoke(cell);
        }
        else
        {
            WrongCellClicked?.Invoke(cell);
        }
    }

    private void OnNextLevelButtonClicked(NextLevelButtonClickedEvent eventData)
    {
        _currentLevelItems = GetLevelItems(eventData.LevelIndex);
    }

    private void OnSetSelected(SetSelectedEvent eventData)
    {
        _selectedSetItems = eventData.SetView.GetSetItems();
        _currentLevelItems = GetLevelItems(eventData.LevelIndex);
    }

    private IReadOnlyList<GameItem> GetLevelItems(int levelIndex)
    {
        var currentLevel = _dataProvider.GetLevel(levelIndex);
        return _selectedSetItems.GetRandomItems(currentLevel.ElementsCount);
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}