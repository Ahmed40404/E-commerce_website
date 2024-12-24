using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    public class LoginController : Controller
    {
        public AppDbcontext db = new AppDbcontext();
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string name,string email,string password, string confirumPassword)
        {
            ViewBag.name = name;
            ViewBag.email = email;
            ViewBag.password = password;
            ViewBag.confirumPassword = confirumPassword;
            if (db.User.Any(e => e.Email == email))
            {
                ViewBag.ErrorMessage = "Email already taken";
                return View();
            }

            if (password != confirumPassword)
            {
                ViewBag.Error = "Password not match confirumed Password";
                return View();
            }
            User user = new User
            {
                Name = name,
                Email = email,
                Password = password,
                Role = "user"
            };
            db.User.Add(user);
            db.SaveChanges();
            ViewBag.Success = "Acount Created";
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string confirumPassword)
        {
            ViewBag.email = email;
            ViewBag.password = password;
            ViewBag.confirumPassword = confirumPassword;
            var user = db.User.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (password != confirumPassword)
            {
                ViewBag.Error = "Password not match confirumed Password";
                return View();
            }
            if (user != null && user.Role=="user") 
            { 
                    return RedirectToAction("Index", "Home");
            }
            if (user != null && user.Role=="admin")
            {
                return RedirectToAction("Index","Products");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
