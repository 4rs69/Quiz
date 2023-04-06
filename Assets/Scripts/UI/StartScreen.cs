using System.Collections.Generic;
using Events;
using GameData;
using UnityEngine;

namespace UI
{
    public class StartScreen : BaseScreen
    {
        [SerializeField] private GameSetView _gameSetViewPrefab;

        [SerializeField] private Transform _iconsParent;

        public void Initialize(IEnumerable<GameSetData> gameSetsData)
        {
            foreach (var gameSetData in gameSetsData)
            {
                var gameSetViewInstance = Instantiate(_gameSetViewPrefab, _iconsParent);
                gameSetViewInstance.SetClickCallback(value =>
                    EventStreams.Game.Publish(new GameSetInstanceClickedEvent(value)));
                gameSetViewInstance.Initialize(gameSetData);
            }
        }
    }
}
