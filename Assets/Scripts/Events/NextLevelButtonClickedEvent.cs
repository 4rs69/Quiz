using SimpleEventBus.Events;

namespace Events
{
    public class NextLevelButtonClickedEvent : EventBase
    {
        public int LevelIndex { get; }

        public NextLevelButtonClickedEvent(int levelIndex)
        {
            LevelIndex = levelIndex;
        }
    }
}