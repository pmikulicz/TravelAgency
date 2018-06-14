using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TravelAgency.Controllers
{
    [Authorize("Admin")]
    public class AdministrationController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}