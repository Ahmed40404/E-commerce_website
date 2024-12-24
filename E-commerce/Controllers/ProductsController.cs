using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    public class ProductsController : Controller
    {
        public AppDbcontext _context = new AppDbcontext();
        public IActionResult Index()
        {
            var product = _context.Product.ToList();
            return View(product);
        }

        public IActionResult Store()
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


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string productName, string description, decimal price, IFormFile file)
        {
            var fpath1 = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fpath = $"wwwroot/images/{fpath1}";
            var filedb = $"images/{fpath1}";
            var filecreate = new FileStream(fpath, FileMode.Create);
            await file.CopyToAsync(filecreate);
            filecreate.Dispose();

            Product product = new Product
            {
                ProductName = productName,
                Description = description,
                Price = price,
                Iamgeurl = filedb
            };
            _context.Product.Add(product);
            _context.SaveChanges();
            return View(product);

        }
        public ActionResult Edit(int id)
        {
            var product = _context.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product model, IFormFile image)
        {
            var product = _context.Product.Find(model.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            
            if (image != null)
            {
 
                var imagesPath = Path.Combine("wwwroot", "images");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                
                var newImageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var newImagePath = Path.Combine(imagesPath, newImageName);

                
                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

               
                if (!string.IsNullOrEmpty(product.Iamgeurl))
                {
                    var oldImagePath = Path.Combine("wwwroot", product.Iamgeurl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                
                product.Iamgeurl = $"images/{newImageName}";
            }

            product.ProductName = model.ProductName;
            product.Description = model.Description;
            product.Price = model.Price;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var oldproduct = _context.Product.Find(id);
            if (oldproduct == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(oldproduct.Iamgeurl))
            {
                var imagePath = Path.Combine("wwwroot", oldproduct.Iamgeurl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Remove(oldproduct);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}