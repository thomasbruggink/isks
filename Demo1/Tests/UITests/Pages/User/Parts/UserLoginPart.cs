using InfoSupport.Tessler.Core;
using OpenQA.Selenium;

namespace UITests.Pages.User.Parts
{
    public class UserLoginPart : PageBase<UserLoginPart>
    {
        public UserLoginPart UserName(string userName)
        {
            var inputField = Driver.GetDriver().FindElement(By.Id("loginUserField"));
            inputField.SendKeys(userName);;
            return this;
        }

        public UserLoginPart Password(string password)
        {
            var inputField = Driver.GetDriver().FindElement(By.Id("loginPasswordField"));
            inputField.SendKeys(password);
            return this;
        }

        public void Register()
        {
            var submitButton = Driver.GetDriver().FindElement(By.Id("loginSubmitButton"));
            submitButton.Click();
        }
    }
}
