using System.Diagnostics;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba1.Firebase;
using Prueba1.Models;

namespace Prueba1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public UserModel GetSessionInfo()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userSession")))
                {
                    UserModel? user = JsonConvert.DeserializeObject<UserModel>(HttpContext.Session.GetString("userSession"));

                    return user;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }


        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Main()
        {
            UserModel? user = GetSessionInfo();

            if (user != null)
            {
                ViewBag.User = user;
                return View();
            }

            return RedirectToAction("Index", "Error");
        }

        public ActionResult Create()
        {
            

            return View();
        }

        

        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public ActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
