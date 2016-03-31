using System.Net.Http;

namespace UITests.TestSupport.Api
{
    public class UserApiClient
    {
        private readonly TestSupportApiHelper _testSupportApiHelper;

        public UserApiClient()
        {
            _testSupportApiHelper = new TestSupportApiHelper();
        }

        public ApiResponse CreateUser(string user, string password)
        {
            var url = "/api/user/create";
            var response = _testSupportApiHelper.Post(url, new
            {
                userName = user,
                password = password
            });

            return response;
        }

        public ApiResponse GetUser(string user)
        {
            var url = $"/api/user/get?name={user}";
            var response = _testSupportApiHelper.Get(url);

            return response;
        }
    }
}
