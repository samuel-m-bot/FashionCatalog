using FashionCatalogue.Data;
using FashionCatalogue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FashionCatalogue.Controllers
{
    [Route("[controller]")]
    public class menController : Controller
    {
        private readonly FashionCatalogContext _context;
        public menController(FashionCatalogContext context)
        {
            _context = context;
        }

        // GET: /Product/
        
        [Route("")]
        public IActionResult Index()
        {
            var products = _context.Products
                  .Where(p => p.Sex == "men")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            int totalCount = _context.Products
                  .Where(p => p.Sex == "men")
                  .ToList().Count();
            ViewBag.TotalCount = totalCount;
            return View(products);

        }
        [Route("t-shirts")]
        public IActionResult t_shirts()
        {
            int totalCount = _context.Products
                  .Where(p => p.Sex == "men" && p.Category == "t-shirts")
                  .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "men" && p.Category == "t-shirts")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);

        }
        [Route("shirts")]
        public IActionResult shirts()
        {
            int totalCount = _context.Products
                  .Where(p => p.Sex == "men" && p.Category == "shirts")
                  .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "men" && p.Category == "shirts")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);

        }
        [Route("hoodies")]
        public IActionResult hoodies()
        {
            int totalCount = _context.Products
                 .Where(p => p.Sex == "men" && p.Category == "hoodies")
                 .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "men" && p.Category == "hoodies")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);
        }
        [Route("trousers")]
        public IActionResult trousers()
        {
            int totalCount = _context.Products
                 .Where(p => p.Sex == "men" && p.Category == "trousers")
                 .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "men" && p.Category == "trousers")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);
        }
        [Route("jeans")]
        public IActionResult jeans()
        {
            int totalCount = _context.Products
                 .Where(p => p.Sex == "men" && p.Category == "jeans")
                 .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "men" && p.Category == "jeans")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);
        }
        [Route("trousers/cargos")]
        public IActionResult Cargos()
        {
            int totalCount = _context.Products
                 .Where(p => p.Sex == "men" && p.SubCategory == "cargo trousers")
                 .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "men" && p.SubCategory == "cargo trousers")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);
        }
        [Route("coats_jackets")]
        public IActionResult coats_jackets()
        {
            int totalCount = _context.Products
                 .Where(p => p.Sex == "men" && p.Category == "coats_jackets")
                 .ToList().Count();
            ViewBag.TotalCount = totalCount;
            var products = _context.Products
                  .Where(p => p.Sex == "men" && p.Category == "coats_jackets")
                  .OrderBy(p => p.Name)
                  .Take(40)
                  .ToList();
            return View(products);
        }
        [Route("GetProductsAll")]
        public ActionResult GetProductsAll(int skip, int take, string category, string store, string price)
        {
            var filteredProducts = _context.Products
                 .Where(p => p.Sex == "men")
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
                 .Where(p => p.Sex == "men")
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
            int []sizearr = new int[2];
            var filteredProducts = _context.Products
                  .Where(p => p.Sex == "men")
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

        [Route("Catagory")]
        public IActionResult ByCatagory(string sex, string catagory)
        {
            return Content(sex + "/" + catagory);
        }
    }
}
