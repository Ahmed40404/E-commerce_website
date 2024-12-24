using System.Diagnostics;
using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    public class HomeController : Controller
    {
       public AppDbcontext _context = new AppDbcontext();

        public IActionResult Index()
        {
            var product = _context.Product.ToList();
            return View(product);
        }



        public IActionResult Details(int id)
        {
            var product = _context.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //----------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------------------//
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
