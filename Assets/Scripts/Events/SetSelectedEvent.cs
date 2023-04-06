using SimpleEventBus.Events;
using UI;

namespace Events
{
    public class SetSelectedEvent : EventBase
    {
        public GameSetView SetView { get; }
        public int LevelIndex { get; }

        public SetSelectedEvent(GameSetView setView, int levelIndex)
        {
            SetView = setView;
            LevelIndex = levelIndex;
        }
    }
}
