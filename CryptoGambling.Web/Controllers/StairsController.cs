using CryptoGambling.Data.DataManaging;
using CryptoGambling.Data.Funds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CryptoGambling.Web.Areas.Identity.Data;

namespace CryptoGambling.Web.Controllers
{
    public class StairsController : Controller
    {
        private readonly IDataManager _dataManager;
        private readonly UserManager<UserIdentity> _userManager;


        public StairsController(IDataManager dataManager, UserManager<UserIdentity> userManager)
        {
            _dataManager = dataManager;
            _userManager = userManager;

        }
        [Authorize]
        public async Task<IActionResult> Index()
        {


            return View();
        }
    }
}
