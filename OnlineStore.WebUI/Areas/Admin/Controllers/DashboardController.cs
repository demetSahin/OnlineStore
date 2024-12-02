using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] //program cs'teki area:exists ile eşleşir.
    [Authorize(Roles = "Admin")] //claimslerdeki claimTypes.Role ile eşleşir.
                                 //Role ile paralel çalışır.
                                 //Admin olmayanlar bu controller'a istek atmayacak.
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
