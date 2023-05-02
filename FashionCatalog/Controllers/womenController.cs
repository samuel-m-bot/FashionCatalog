using FashionCatalogue.Data;
using FashionCatalogue.Models;
using Microsoft.AspNetCore.Mvc;

namespace FashionCatalogue.Controllers
{
    [Route("[controller]")]
    public class womenController : Controller
    {
        private readonly FashionCatalogContext _context;
        public womenController(FashionCatalogContext context)
        {
            _context = context;
        }
        [Route("")]
        public IActionResult Index()
        {
            var products = _context.Products
                 .Where(p => p.Sex == "women")
                 .OrderBy(p => p.Name)
                 .Take(40)
                 .ToList();
            int totalCount = _context.Products
                  .Where(p => p.Sex == "women")
                  .ToList().Count();
            ViewBag.TotalCount = totalCount;
            return View(products);
        }
        [Route("dresses")]
        public IActionResult dresses()
        {
            int totalCount = _context.Products
                   .Where(p => p.Sex == "women" && p.Category == "dresses")
                   .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "women" && p.Category == "dresses")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);
        }
        [Route("tops")]
        public IActionResult tops()
        {
            int totalCount = _context.Products
                  .Where(p => p.Sex == "women" && p.Category == "tops")
                  .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "women" && p.Category == "tops")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);
        }
        [Route("trousers")]
        public IActionResult trousers()
        {
            int totalCount = _context.Products
                  .Where(p => p.Sex == "women" && p.Category == "trousers")
                  .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "women" && p.Category == "trousers")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);
        }
        [Route("coats-jackets")]
        public IActionResult coats_jackets()
        {
            int totalCount = _context.Products
                  .Where(p => p.Sex == "women" && p.Category == "coats_jackets")
                  .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "women" && p.Category == "coats_jackets")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);
        }
        [Route("jeans")]
        public IActionResult jeans()
        {
            int totalCount = _context.Products
                  .Where(p => p.Sex == "women" && p.Category == "jeans")
                  .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "women" && p.Category == "jeans")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);
        }
        [Route("GetProductsAll")]
        public ActionResult GetProductsAll(int skip, int take, string category, string store, string price)
        {
            var filteredProducts = _context.Products
                 .Where(p => p.Sex == "women")
                 .OrderBy(p => p.Name)
                 .ToList();

            // Apply filters
            if (!string.IsNullOrEmpty(store))
            {
                filteredProducts = filteredProducts.Where(p => p.Store == store).ToList();
            }
            if (!string.IsNullOrEmpty(category))
            {
                filteredProducts = filteredProducts.Where(p => p.Category == category).ToList();
            }

            if (!string.IsNullOrEmpty(price))
            {
                if (price == "lowToHigh")
                {
                    filteredProducts = filteredProducts.OrderBy(p => p.Price).ToList();
                }
                else if (price == "highToLow")
                {
                    filteredProducts = filteredProducts.OrderByDescending(p => p.Price).ToList();
                }
            }
            filteredProducts = filteredProducts.Skip(skip).Take(take).ToList();

            // Return the partial view with the fetched products
            return PartialView("_ProductListAll", filteredProducts);
        }
        [Route("GetProducts")]
        public ActionResult GetProducts(int skip, int take, string category, string type, string store, string price)
        {
            var filteredProducts = _context.Products
                 .Where(p => p.Sex == "women")
                 .OrderBy(p => p.Name)
                 .ToList();

            // Apply filters
            if (!string.IsNullOrEmpty(store))
            {
                filteredProducts = filteredProducts.Where(p => p.Store == store).ToList();
            }
            if (!string.IsNullOrEmpty(category))
            {
                filteredProducts = filteredProducts.Where(p => p.Category == category).ToList();
            }
            if (!string.IsNullOrEmpty(type))
            {
                filteredProducts = filteredProducts.Where(p => p.SubCategory == type).ToList();
            }

            if (!string.IsNullOrEmpty(price))
            {
                if (price == "lowToHigh")
                {
                    filteredProducts = filteredProducts.OrderBy(p => p.Price).ToList();
                }
                else if (price == "highToLow")
                {
                    filteredProducts = filteredProducts.OrderByDescending(p => p.Price).ToList();
                }
            }
            filteredProducts = filteredProducts.Skip(skip).Take(take).ToList();

            // Return the partial view with the fetched products
            return PartialView("_ProductList", filteredProducts);
        }
        [Route("GetProductsList")]
        [HttpGet]
        public int[] GetProductsList(int skip, int take, string category, string type, string store, string price)
        {
            int[] sizearr = new int[2];
            var filteredProducts = _context.Products
                 .Where(p => p.Sex == "women")
                 .OrderBy(p => p.Name)
                 .ToList();
            // Apply filters
            if (!string.IsNullOrEmpty(store))
            {
                filteredProducts = filteredProducts.Where(p => p.Store == store).ToList();
            }
            if (!string.IsNullOrEmpty(category))
            {
                filteredProducts = filteredProducts.Where(p => p.Category == category).ToList();
            }
            if (!string.IsNullOrEmpty(type))
            {
                filteredProducts = filteredProducts.Where(p => p.SubCategory == type).ToList();
            }

            if (!string.IsNullOrEmpty(price))
            {
                if (price == "lowToHigh")
                {
                    filteredProducts = filteredProducts.OrderBy(p => p.Price).ToList();
                }
                else if (price == "highToLow")
                {
                    filteredProducts = filteredProducts.OrderByDescending(p => p.Price).ToList();
                }
            }
            sizearr[0] = filteredProducts.Count;
            filteredProducts = filteredProducts.Skip(skip).Take(take).ToList();
            List<Product> temp = filteredProducts.ToList();
            sizearr[1] = temp.Count;
            // Return the partial view with the fetched products
            return sizearr;
        }
    }
}
