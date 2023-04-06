using IngameStateMachine;
using UI;
using UnityEngine;

namespace States
{
    public class LevelCompletedState : MonoBehaviour, IState
    {
        [SerializeField] private LevelCompletedScreen _levelCompletedScreen;

        public void OnEnter()
        {
            _levelCompletedScreen.Show();
            _levelCompletedScreen.ShowNextLevelButton();
        }

        public void OnExit()
        {
            _levelCompletedScreen.Hide();
        }
    }
}