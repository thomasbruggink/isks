using System.Collections.Generic;
using Business.Services;
using Engine.EventHandlers;
using Engine.Tests.Helpers;
using TechTalk.SpecFlow;

namespace Engine.Tests.Handlers
{
    [Binding]
    public class BeforeScenarioHandler
    {
        [BeforeScenario]
        public void SetupEventListener()
        {
            var eventListener = new EventListener();
            var listeners = new List<IEventHandler>
            {
                new BlogEventHandler(),
                new UserEventHandler()
            };
            listeners.ForEach(eh => eventListener.SubscribeEventHandler(eh));
            EventListenerHelper.SaveEventListener(eventListener);
        }

        [BeforeScenario]
        public static void ClearDatabase()
        {
            var connection = NeoConnection.GetConnection();
            connection.GraphClient.Cypher.Match("(a)")
                .OptionalMatch("(a)-[b]-()")
                .Delete("a,b")
                .ExecuteWithoutResults();
        }
    }
}