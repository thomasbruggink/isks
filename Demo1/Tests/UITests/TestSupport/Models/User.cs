using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UITests.TestSupport.Api;

namespace UITests.TestSupport.Models
{
    public class User
    {
        public string UserName { get; set; }

        public static User CreateUser(string username, string password)
        {
            var userApiClient = new UserApiClient();

            var response = userApiClient.CreateUser(username, password);
            Assert.AreEqual(HttpStatusCode.OK, response.Status, $"Unable to create user '{username}'");

            return new User
            {
                UserName = response.Content.userName
            };
        }

        public static User GetUserByName(string username)
        {
            var userApiClient = new UserApiClient();

            var response = userApiClient.GetUser(username);
            Assert.AreEqual(HttpStatusCode.OK, response.Status, $"Unable to find user {username}");

            return new User
            {
                UserName = response.Content.userName
            };
        }
    }
}