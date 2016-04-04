using Business.Services;

namespace Business.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        public void CreateUser(string username)
        {
            var neoConnection = NeoConnection.GetConnection();

            neoConnection.GraphClient.Cypher.Create("(u:User {Name: {name}})")
                .WithParam("name", username)
                .ExecuteWithoutResults();
        }
    }
}
