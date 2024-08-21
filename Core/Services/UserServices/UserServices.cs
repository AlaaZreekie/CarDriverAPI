using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services.UserServices
{
    public class UserServices(UserManager<IdentityUser> userManeger, IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<IdentityUser> _userManeger = userManeger;
        public async Task<string> LogIn(UserDTO u)
        {
            if (u == null) { throw new Exception("ERROR"); }
            if (u.Email == null) { throw new Exception("Email ERROR"); }
            var user = await _userManeger.FindByEmailAsync(u.Email);
            if (user == null) { throw new Exception("User Not Found"); }
            if (u.Password == null || string.IsNullOrEmpty(u.Password) ){ throw new Exception("You have to provide password"); }  
            var res = await _userManeger.CheckPasswordAsync(user, u.Password);
            if (!res) { throw new Exception("The Password Is Not Correct"); }
            else
            {   
                u.Name = user.UserName;
                u.Password = "";
                var claims = new List<Claim>();
                var Sub = _configuration["Jwt:Subject"];
                if(Sub == null) { Sub = ""; }
                if(u.Name ==  null ) { u.Name = ""; }
                var c1 = new Claim(JwtRegisteredClaimNames.Sub, Sub);
                var c2 = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
                var c3 = new Claim("User", u.Name.ToString());
                var c4 = new Claim("email", u.Email.ToString());
                claims.Add(c1);
                claims.Add(c2);
                claims.Add(c3);
                claims.Add(c4);
                var KeyCheck = _configuration["Jwt:Key"];
                if (KeyCheck == null) { KeyCheck = ""; }
                var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KeyCheck));
                var sighIn = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: sighIn);
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenValue;
            }

        }
        public async Task<UserDTO> Register(UserDTO? u)
        {
            if (u == null) throw new Exception("Enter Email and Password");
            if(u.Email == "" || u.Email == null) { throw new Exception("Enter Email"); }
            if (u.Password == "" || u.Password == null) { throw new Exception("Enter Email"); }
            var user = await  _userManeger.FindByEmailAsync(u.Email);
            if (user != null) { throw new Exception("Invalid user"); }
            user = new IdentityUser();
            user.Email = u.Email;
            user.UserName = u.Name;
            //user.PasswordHash = _userManeger.AddPasswordAsync();
            var reult = await _userManeger.CreateAsync(user,u.Password);
            return new UserDTO() {Name = u.Name, Email = u.Email };
        }
        /*public UserDTO ChangeRole()
        {
            return null;
        }*/
    }
}
