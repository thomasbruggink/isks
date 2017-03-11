using System;
using System.Web;
using System.Web.Mvc;
using Business.Repositories;
using Demo1.Models;
using Serilog;

namespace Demo1.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            var user = _userRepository.Login(loginModel.UserName, loginModel.Password);
            if (user == null)
            {
                Log.Information("{username} login failed", loginModel.UserName);
                ViewBag.Error = "Invalid username or password";
                return View("Index");
            }
            var loginCookie = new HttpCookie("user", user.UserName);
            Response.Cookies.Add(loginCookie);
            ViewBag.Message = $"Welcome {user.UserName}!";
            return View("Message");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            if (Request.Cookies["user"] != null)
            {
                var loginCookie = new HttpCookie("user") {Expires = DateTime.Now.AddDays(-1)};
                Response.Cookies.Add(loginCookie);
            }
            ViewBag.Message = "Logged out";
            return View("Message");
        }

        [HttpPost]
        public ActionResult Register(LoginModel loginModel)
        {
            var user = _userRepository.Register(loginModel.UserName, loginModel.Password);
            if (user == null)
            {
                ViewBag.Error = "Account creation failed";
                return View("Index");
            }
            ViewBag.Message = "Account created!";
            return View("Message");
        }

        [HttpGet]
        public ActionResult Message()
        {
            return View();
        }
    }
}