using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using UITests.Pages;
using UITests.Pages.User;

namespace UITests.Bindings.Then
{
    [Binding]
    public class ThenMessage
    {
        [Then(@"I see the '(.*)' message")]
        public void ThenISeeTheMessage(string text)
        {
            var messagePage = NavigationHelper.ResolvePage<MessagePage>();
            Assert.AreEqual(text, messagePage.GetMessage(), "Expected a different message");
        }

    }
}
