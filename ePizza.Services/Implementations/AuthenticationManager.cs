using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ePizza.Entities.Concrete;
using ePizza.Entities.Dtos;
using ePizza.Services.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace ePizza.Services.Implementations
{
    public class AuthenticationManager: IAuthenticationService
    {
        protected SignInManager<User> _signManager;
        protected UserManager<User> _userManager;
        protected RoleManager<Role> _roleManager;
  
        public AuthenticationManager(SignInManager<User> signManager, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
      
            _signManager = signManager;
        }
        public bool CreateUser(User user, string password)
        {
        
            var result = _userManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                //Admin,User 1 burası dinamik olacaktır....
                string role = "User";
                //string role = "Admin";
                var res = _userManager.AddToRoleAsync(user, role).Result;
                if (res.Succeeded)
                {
                    return true;
                }

            }
            return false;
        }

        public async Task<bool> SignOut()
        {
            try
            {
                await _signManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

  
        public User AuthenticateUser(string userName, string password)
        {
            var result = _signManager.PasswordSignInAsync(userName, password, false, lockoutOnFailure: false).Result;
            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(userName).Result;
                var roles = _userManager.GetRolesAsync(user).Result;
                user.Roles = roles.ToArray();

                return user;
            }
            return null;

        }

        public User GetUser(string username)
        {
            return _userManager.FindByNameAsync(username).Result;
        }
    }
}
