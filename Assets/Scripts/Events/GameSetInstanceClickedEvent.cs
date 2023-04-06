using SimpleEventBus.Events;
using UI;

namespace Events
{
    public class GameSetInstanceClickedEvent : EventBase
    {
        public GameSetView GameSetView { get; }

        public GameSetInstanceClickedEvent(GameSetView gameSetView)
        {
            GameSetView = gameSetView;
        }
    }
}