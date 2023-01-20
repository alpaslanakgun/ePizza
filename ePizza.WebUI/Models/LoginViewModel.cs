using System.ComponentModel.DataAnnotations;

namespace ePizza.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
    }
}
