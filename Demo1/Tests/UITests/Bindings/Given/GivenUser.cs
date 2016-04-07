using TechTalk.SpecFlow;
using UITests.TestSupport.Models;

namespace UITests.Bindings.Given
{
    [Binding]
    public class GivenUser
    {
        [Given(@"The following accounts are created")]
        public void GivenTheFollowingAccountsAreCreated(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                var userName = tableRow["UserName"];
                var password = KnownUsers.Get(userName).Password;

                User.CreateUser(userName, password);
            }
        }
    }
}
