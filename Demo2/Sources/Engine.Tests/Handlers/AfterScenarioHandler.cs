using Engine.Tests.Helpers;
using TechTalk.SpecFlow;

namespace Engine.Tests.Handlers
{
    [Binding]
    public class AfterScenarioHandler
    {
        [AfterScenario]
        public void CleanupEventLister()
        {
            EventListenerHelper.DestroyEventListener();
        }
    }
}