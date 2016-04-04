using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Repositories.Blog;
using Newtonsoft.Json;

namespace Service.Controllers
{
    public class ReadController : ApiController
    {
        private readonly IBlogRepository _blogRepository;

        public ReadController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public HttpResponseMessage UserReads(string user)
        {
            var blogsRead = _blogRepository.GetReadsForUser(user);

            return Request.CreateResponse(HttpStatusCode.OK, blogsRead);
        }
    }
}