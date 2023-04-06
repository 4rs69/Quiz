using Events;
using GameData;
using IngameStateMachine;
using JetBrains.Annotations;
using SimpleEventBus.Disposables;
using UnityEngine;
using States;


public class GameController : MonoBehaviour
{
    [SerializeField] private DataProvider _dataProvider;

    private StateMachine _stateMachine;
    private int _currentLevelIndex;
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        var states = new IState[]
        {
            GetComponent<StartGameState>(),
            GetComponent<GameState>(),
            GetComponent<LevelCompletedState>(),
            GetComponent<GameOverState>()
        };
        _stateMachine = new StateMachine(states);
        _stateMachine.Enter<StartGameState>();

        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameSetInstanceClickedEvent>(OnGameSetInstanceClicked),
            EventStreams.Game.Subscribe<LevelCompletedEvent>(CheckGameOver)
        };
    }

    private void OnGameSetInstanceClicked(GameSetInstanceClickedEvent eventData)
    {
        EventStreams.Game.Publish(new SetSelectedEvent(eventData.GameSetView, _currentLevelIndex));
        _stateMachine.Enter<GameState>();
    }

    [UsedImplicitly]
    // from click on NextLevel in LevelCompletedScreen
    public void LoadNextLevel()
    {
        _currentLevelIndex++;
        EventStreams.Game.Publish(new NextLevelButtonClickedEvent(_currentLevelIndex));
        _stateMachine.Enter<GameState>();
    }

    [UsedImplicitly]
    // from click on MainMenuButton
    public void RestartGame()
    {
        _currentLevelIndex = 0;
        _stateMachine.Enter<StartGameState>();
    }

    private void CheckGameOver(LevelCompletedEvent eventData)
    {
        if (IsLastLevel())
        {
            _stateMachine.Enter<GameOverState>();
        }
        else
        {
            _stateMachine.Enter<LevelCompletedState>();
        }
    }

    private bool IsLastLevel()
    {
        return _currentLevelIndex == _dataProvider.LevelsCount() - 1;
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}


