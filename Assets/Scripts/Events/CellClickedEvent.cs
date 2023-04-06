using SimpleEventBus.Events;

namespace Events
{
    public class CellClickedEvent : EventBase
    {
        private Cell Cell { get; }

        public CellClickedEvent(Cell cell)
        {
            Cell = cell;
        }
    }
}