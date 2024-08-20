using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;

namespace Core.Services.UserServices
{
    public class UserServices(UserManager<IdentityUser> userManeger)
    {
        private readonly UserManager<IdentityUser> _userManeger = userManeger;
        public async Task<UserDTO> LogIn(UserDTO u)
        {
            var user = await _userManeger.FindByEmailAsync(u.Email);
            if (user == null) { throw new Exception("User Not Found"); }
            var res = await _userManeger.CheckPasswordAsync(user, u.Password);
            if (!res) { throw new Exception("The Passward Is Not Correct"); }
            else
            {   
                u.Name = user.UserName;
                u.Password = "";
                return u;
            }

        }
        public async Task<UserDTO> Register(UserDTO u)
        {
            var user = await  _userManeger.FindByEmailAsync(u.Email);
            if (user != null) { return null; }
            user = new IdentityUser();
            user.Email = u.Email;
            user.UserName = u.Name;
            //user.PasswordHash = _userManeger.AddPasswordAsync();
            var reult = await _userManeger.CreateAsync(user,u.Password);
            
            u.Password = "";
            return new UserDTO() { Email = u.Email };
        }
        public UserDTO ChangeRole()
        {
            return null;
        }
    }
}
