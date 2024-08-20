using Core.DTO;
using Core.Services.UserServices;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Properties.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController(UserServices userServices, IConfiguration configuration) : Controller
{
    private readonly UserServices _userServices = userServices;
    private readonly IConfiguration _configuration = configuration;
    [HttpPost]
    public async Task<IActionResult> LogIn([FromForm] string email, [FromForm] string passward)
    {
        UserDTO user = new() { Email = email, Password = passward };
        var res = new UserDTO();
        res = await _userServices.LogIn(user);

        if (res == null) { return BadRequest("Invalid Passward"); }
        else
        {
            var claims = new List<Claim>();            
            var c1 = new Claim(JwtRegisteredClaimNames.Sub, value: _configuration["Jwt:Subject"]);
            var c2 = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            var c3 = new Claim("User", res.Name.ToString());
            var c4 = new Claim("email", res.Email.ToString());
            claims.Add(c1);
            claims.Add(c2);
            claims.Add(c3);
            claims.Add(c4);
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var sighIn = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: sighIn

            );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new {Token = tokenValue });
        }        
    }
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] string email, [FromForm] string passward,string name )
    {
        UserDTO user = new() { Email = email, Password = passward , Name = name};
        var res =await _userServices.Register(user);
        if (res == null) { return BadRequest("Invalid email"); }                
        return Ok(res);
    }

}
