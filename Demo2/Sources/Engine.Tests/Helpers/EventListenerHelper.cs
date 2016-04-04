using System.Linq;
using TechTalk.SpecFlow;

namespace Engine.Tests.Helpers
{
    public class EventListenerHelper
    {
        private const string Key = "eventListener";

        public static EventListener GetEventListener()
        {
            var eventListener = (EventListener)ScenarioContext.Current[Key];
            return eventListener;
        }

        public static void SaveEventListener(EventListener eventListener)
        {
            ScenarioContext.Current[Key] = eventListener;
        }

        public static void DestroyEventListener()
        {
            var eventListener = (EventListener) ScenarioContext.Current[Key];
            eventListener.EventHandlers.ToList().ForEach(eh => eventListener.UnsubscribeEventHandler(eh));
        }
    }
}
