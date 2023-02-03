using ePizza.WebUI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePizza.WebUI.Areas.User.Controllers;

namespace ePizza.WebUI.Areas.User.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController(IUserAccessor userAccessor) : base(userAccessor)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
