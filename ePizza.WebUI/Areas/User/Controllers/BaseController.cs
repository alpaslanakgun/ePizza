using ePizza.WebUI.Helpers;
using ePizza.WebUI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ePizza.WebUI.Helpers;
using ePizza.WebUI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.Areas.User.Controllers
{
    [CustomAuthorize(Roles = "User")]
    [Area("User")]
    public class BaseController : Controller
    {
        public ePizza.Entities.Concrete.User CurrentUser
        {
            get
            {
                if (User != null)
                    return _userAccessor.GetUser();
                else
                    return null;
            }
        }

        IUserAccessor _userAccessor;
        public BaseController(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }
    }
}
