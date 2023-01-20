using ePizza.Entities;
using ePizza.Entities.Concrete;
using ePizza.WebUI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public User CurrentUser
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
