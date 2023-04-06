using UI;
using IngameStateMachine;
using UnityEngine;

namespace States
{
    public class GameOverState : MonoBehaviour, IState
    {
        [SerializeField] private LevelCompletedScreen _levelCompletedScreen;

        public void OnEnter()
        {
            _levelCompletedScreen.Show();
            _levelCompletedScreen.ShowRestartView();
        }

        public void OnExit()
        {
            _levelCompletedScreen.Hide();
        }

    }
}