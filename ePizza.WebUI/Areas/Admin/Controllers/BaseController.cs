using ePizza.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.WebUI.Area.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    [Area("Admin")]
    public class BaseController : Controller
    {

    }
}
