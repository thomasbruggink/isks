using Service.Tests.Helpers;
using TechTalk.SpecFlow;

namespace Service.Tests.Bindings.When
{
    [Binding]
    public class WhenBlog
    {
        [When(@"'(.*)' requests his reads")]
        public void WhenRequestsHisReads(string user)
        {
            TestWebSender.Send(webClient =>
            {
                var uri = $"/api/read/{user}";
                var response = webClient.GetAsync(uri).Result;
                ApiResultTable.Instance.AddResult(user, response);
            });
        }
    }
}