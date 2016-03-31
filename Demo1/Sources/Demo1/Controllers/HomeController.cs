using System.Linq;
using System.Web.Mvc;
using Business.Repositories;

namespace Demo1.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogRepository _blogRepository;

        public HomeController()
        {
            _blogRepository = new BlogRepository();
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(_blogRepository.GetBlogs().OrderByDescending(b => b.CreatedTime).ToList());
        }
    }
}