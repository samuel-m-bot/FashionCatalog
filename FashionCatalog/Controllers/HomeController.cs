using FashionCatalogue.Data;
using FashionCatalogue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;

namespace FashionCatalogue.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FashionCatalogContext _context;

        public HomeController(ILogger<HomeController> logger, FashionCatalogContext context)
        {
            _context = context;
            _logger = logger;
        }
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("GetRandomProductImage")]
        [HttpGet]
        public List<Product> GetRandomProductImage()
        {
            // Get all products from the database
            List<Product> allProducts = _context.Products
                  .Where(p => p.Sex == "men")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            List<Product> allProducts2= _context.Products
                  .Where(p => p.Sex == "women")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            // Get a random product from the list of all products
            Product randomProduct = allProducts[new Random().Next(0, allProducts.Count)];
            Product randomProduct2 = allProducts2[new Random().Next(0, allProducts2.Count)];

            List<Product> ranProducts = new List<Product>();
            ranProducts.Add(randomProduct);
            ranProducts.Add(randomProduct2);
            // Return the product object
            return ranProducts;
        }
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}