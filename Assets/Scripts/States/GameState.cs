using System;
using IngameStateMachine;
using UI;
using UnityEngine;

namespace States
{
    public class GameState : MonoBehaviour, IState
    {
        [SerializeField] private GameScreen _gameScreen;

        [SerializeField] private LevelController _levelController;

        private void Awake()
        {
            _levelController.GoalSelected += _gameScreen.SetGoal;
            _levelController.WrongCellClicked += _gameScreen.OnWrongCellClicked;
            _levelController.TargetCellClicked += _gameScreen.OnTargetCellClicked;
        }

        public void OnEnter()
        {
            _gameScreen.Show();
            _levelController.StartLevel();
        }

        public void OnExit()
        {
            _gameScreen.Hide();
        }

        public void OnDestroy()
        {
            _levelController.GoalSelected -= _gameScreen.SetGoal;
            _levelController.WrongCellClicked -= _gameScreen.OnWrongCellClicked;
            _levelController.TargetCellClicked -= _gameScreen.OnTargetCellClicked;
        }
    }
}