namespace UITests.TestSupport.Api
{
    public class BlogApiClient
    {
        private readonly TestSupportApiHelper _testSupportApiHelper;

        public BlogApiClient()
        {
            _testSupportApiHelper = new TestSupportApiHelper();
        }

        public ApiResponse CreateBlog(string title, string content, string writer)
        {
            var url = "/api/blog/create";

            var response = _testSupportApiHelper.Post(url, new
            {
                title = title,
                content = content,
                writer = writer
            });

            return response;
        }
    }
}
