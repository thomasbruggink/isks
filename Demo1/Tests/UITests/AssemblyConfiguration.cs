using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UITests
{
    [TestClass]
    public class AssemblyConfiguration
    {
        [AssemblyInitialize]
        public static void ConfigureAssembly(TestContext testContext)
        {
            TesslerState.AssemblyInitialize();
            TesslerState.Configure()
                .SetWebsiteUrl("http://localhost:49854/")
                .SetBrowser(Browser.Chrome)
                .SetMaximizeBrowser(true);
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
            TesslerState.AssemblyCleanup();
        }
    }
}