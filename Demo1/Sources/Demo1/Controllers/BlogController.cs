using System.Web.Mvc;
using Business.Repositories;
using Demo1.Models;

namespace Demo1.Controllers
{
    public class BlogController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly BlogRepository _blogRepository;

        public BlogController()
        {
            _blogRepository = new BlogRepository();
            _userRepository = new UserRepository();
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BlogModel blogModel)
        {
            if (Request.Cookies["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = _userRepository.GetUserByName(Request.Cookies["user"].Value);

            _blogRepository.CreateBlog(user, blogModel.Title, blogModel.Content);            

            return RedirectToAction("Index", "Home");
        }
    }
}