using CryptoGambling.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CryptoGambling.Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }




    }
}