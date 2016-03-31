using TechTalk.SpecFlow;
using UITests.Pages;
using UITests.Pages.User;

namespace UITests.Bindings.When
{
    [Binding]
    public class WhenUser
    {
        [When(@"I create an account as '(.*)'")]
        public void WhenICreateAnAccountAs(string userName)
        {
            var userPage = NavigationHelper.Navigate<UserPage>();

            var blogPostUser = KnownUsers.Get(userName);

            userPage.RegisterPart()
                .UserName(blogPostUser.UserName)
                .Password(blogPostUser.Password)
                .Register();
        }


        [When(@"I login as '(.*)'")]
        public void WhenILoginAs(string userName)
        {
            var userPage = NavigationHelper.Navigate<UserPage>();

            var blogPostUser = KnownUsers.Get(userName);

            userPage.LoginPart()
                .UserName(blogPostUser.UserName)
                .Password(blogPostUser.Password)
                .Register();
        }

    }
}
