using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Repositories;

namespace TestSupport.api
{
    [AllowAnonymous]
    public class UserController : ApiController
    {
        private readonly UserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        [HttpGet]
        public HttpResponseMessage Get(string name)
        {
            var user = _userRepository.GetUserByName(name);

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                userName = user
            });
        }

        [HttpPost]
        public HttpResponseMessage Create([FromBody] dynamic data)
        {
            var userName = (string) data.userName;
            var password = (string) data.password;

            var user = _userRepository.Register(userName, password);

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                userName = user.UserName
            });
        }
    }
}