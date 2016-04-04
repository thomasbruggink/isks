using Service.Tests.Helpers;
using TechTalk.SpecFlow;

namespace Service.Tests.Handles
{
    [Binding]
    public class BeforeScenario
    {
        [BeforeScenario]
        public static void ResetApiResultHelper()
        {
            ApiResultTable.Reset();
        }
    }
}
