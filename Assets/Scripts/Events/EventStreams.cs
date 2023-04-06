using SimpleEventBus;
using SimpleEventBus.Interfaces;

namespace Events
{
    public static class EventStreams
    {
        public static IEventBus Game { get; } = new EventBus();

        public static IEventBus AIQuiz { get; } = new EventBus();
    }
}