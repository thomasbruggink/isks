using OpenQA.Selenium;

namespace UITests.Pages.User
{
    public class MessagePage : PageBase<MessagePage>, IPage
    {
        /// <summary>
        /// Should not be used
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            return null;
        }

        public string GetMessage()
        {
            var message = AssemblyConfiguration.Driver.FindElement(By.Id("responseMessage"));
            return message.Text;
        }
    }
}
