using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Repositories;

namespace TestSupport.api
{
    public class BlogController : ApiController
    {
        private readonly UserRepository _userRepository;
        private readonly BlogRepository _blogRepository;

        public BlogController()
        {
            _userRepository = new UserRepository();
            _blogRepository = new BlogRepository();
        }

        [HttpPost]
        public HttpResponseMessage Create([FromBody] dynamic data)
        {
            var title = (string) data.title;
            var content = (string) data.content;
            var writer = (string) data.writer;

            var user = _userRepository.GetUserByName(writer);

            _blogRepository.CreateBlog(user, title, content);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}