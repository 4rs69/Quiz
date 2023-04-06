using SimpleEventBus.Events;

namespace Events
{
    public class LevelCompletedEvent : EventBase
    {
        public Cell Cell { get; }

        public LevelCompletedEvent(Cell cell)
        {
            Cell = cell;
        }
    }
}