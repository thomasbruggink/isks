using Engine.Tests.Helpers;
using TechTalk.SpecFlow;

namespace Engine.Tests.Bindings.When
{
    [Binding]
    public class WhenBlog
    {
        [When(@"The following blog created events are send")]
        public void WhenTheFollowingBlogCreatedEventsAreSend(Table table)
        {
            var eventListener = EventListenerHelper.GetEventListener();

            foreach (var tableRow in table.Rows)
            {
                var name = tableRow["Name"];
                var writer = tableRow["Writer"];

                var blogCreatedEvent = EventHelper.CreateEvent("blogCreated", new {name, writer});

                eventListener.HandleMessage(blogCreatedEvent);
            }
        }

        [When(@"The following blog read events are send")]
        public void WhenTheFollowingBlogReadEventsAreSend(Table table)
        {
            var eventListener = EventListenerHelper.GetEventListener();

            foreach (var tableRow in table.Rows)
            {
                var reader = tableRow["Reader"];
                var blogName = tableRow["Blog Name"];

                var blogReadEvent = EventHelper.CreateEvent("blogRead", new {reader, blogName});

                eventListener.HandleMessage(blogReadEvent);
            }
        }
    }
}