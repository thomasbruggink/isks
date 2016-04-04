using Engine.Tests.Helpers;
using TechTalk.SpecFlow;

namespace Engine.Tests.Bindings.When
{
    [Binding]
    public class WhenUser
    {
        [When(@"The following user created event is send")]
        public void WhenTheFollowingUserCreatedEventIsSend(Table table)
        {
            var eventHandler = EventListenerHelper.GetEventListener();

            foreach (var tableRow in table.Rows)
            {
                var name = tableRow["Name"];

                var userCreatedEvent = EventHelper.CreateEvent("userCreated", new {name});

                eventHandler.HandleMessage(userCreatedEvent);
            }
        }
    }
}