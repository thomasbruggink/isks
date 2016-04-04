using Business.Repositories.Users;
using TechTalk.SpecFlow;

namespace Engine.Tests.Bindings.Given
{
    [Binding]
    public class GivenUser
    {
        [Given(@"The following user created events have been sent")]
        public void GivenTheFollowingUserCreatedEventsHaveBeenSent(Table table)
        {
            var userRepository = new UserRepository();

            foreach (var tableRow in table.Rows)
            {
                var user = tableRow["Name"];

                userRepository.CreateUser(user);
            }
        }
    }
}