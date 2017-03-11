using OpenQA.Selenium;

namespace UITests.Pages.User.Parts
{
    public class UserRegisterPart : PageBase<UserRegisterPart>
    {
        public UserRegisterPart UserName(string userName)
        {
            var inputField = AssemblyConfiguration.Driver.FindElement(By.Id("registerUserField"));
            inputField.SendKeys(userName);;
            return this;
        }

        public UserRegisterPart Password(string password)
        {
            var inputField = AssemblyConfiguration.Driver.FindElement(By.Id("registerPasswordField"));
            inputField.SendKeys(password);
            return this;
        }

        public void Register()
        {
            var submitButton = AssemblyConfiguration.Driver.FindElement(By.Id("registerSubmitButton"));
            submitButton.Click();
        }
    }
}
