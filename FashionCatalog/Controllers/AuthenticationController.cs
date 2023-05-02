using FashionCatalogue.Data;
using FashionCatalogue.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace FashionCatalogue.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private FashionCatalogContext _context;
        public AuthenticationController(FashionCatalogContext context)
        {
            _context = context;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        //[HttpPost]
        //[Route("Login")]
        //public IActionResult Login(string username, string password)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Username == username);
        //    if (user != null && VerifyPasswordHash(password, user.PasswordHash))
        //    {
        //        // Credentials are valid, create a session for the user
        //        //Session["UserId"] = user.Id;
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        // Credentials are invalid, show an error message
        //        ModelState.AddModelError("", "Invalid username or password");
        //        return View();
        //    }
        //}
        private bool VerifyPasswordHash(string password, string hash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return hash == hashedPassword;
            }
        }
        //[HttpPost]
        //public IActionResult RegisterIn(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Check if the username or email address is already taken
        //        if (_context.Users.Any(u => u.Username == model.Username || u.Email == model.Email))
        //        {
        //            ModelState.AddModelError("", "Username or email address is already taken");
        //            return View(model);
        //        }

        //        // Hash the password and save the new user to the database
        //        var user = new Users
        //        {
        //            Username = model.Username,
        //            Email = model.Email,
        //            PasswordHash = HashPassword(model.Password)
        //        };
        //        _context.Users.Add(user);
        //        _context.SaveChanges();

        //        // Log in the new user and redirect to the home page
        //        //Session["UserId"] = user.Id;
        //        return RedirectToAction("Index", "Home");
        //    }

        //    return View(model);
        //}

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return hashedPassword;
            }
        }
    }
}
