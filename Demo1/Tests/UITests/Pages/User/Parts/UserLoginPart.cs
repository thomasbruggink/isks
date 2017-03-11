using OpenQA.Selenium;

namespace UITests.Pages.User.Parts
{
    public class UserLoginPart : PageBase<UserLoginPart>
    {
        public UserLoginPart UserName(string userName)
        {
            var inputField = AssemblyConfiguration.Driver.FindElement(By.Id("loginUserField"));
            inputField.SendKeys(userName);;
            return this;
        }

        public UserLoginPart Password(string password)
        {
            var inputField = AssemblyConfiguration.Driver.FindElement(By.Id("loginPasswordField"));
            inputField.SendKeys(password);
            return this;
        }

        public void Register()
        {
            var submitButton = AssemblyConfiguration.Driver.FindElement(By.Id("loginSubmitButton"));
            submitButton.Click();
        }
    }
}
