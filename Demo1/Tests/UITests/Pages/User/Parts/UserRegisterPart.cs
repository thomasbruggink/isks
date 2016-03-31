using InfoSupport.Tessler.Core;
using OpenQA.Selenium;

namespace UITests.Pages.User.Parts
{
    public class UserRegisterPart : PageBase<UserRegisterPart>
    {
        public UserRegisterPart UserName(string userName)
        {
            var inputField = Driver.GetDriver().FindElement(By.Id("registerUserField"));
            inputField.SendKeys(userName);;
            return this;
        }

        public UserRegisterPart Password(string password)
        {
            var inputField = Driver.GetDriver().FindElement(By.Id("registerPasswordField"));
            inputField.SendKeys(password);
            return this;
        }

        public void Register()
        {
            var submitButton = Driver.GetDriver().FindElement(By.Id("registerSubmitButton"));
            submitButton.Click();
        }
    }
}
