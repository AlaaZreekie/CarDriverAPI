using Core.DTO;
using Core.Services.UserServices;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers;

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
        var res = await _userServices.LogIn(user);
        if (res == null || string.IsNullOrEmpty(res)) { return BadRequest("ERROR"); }
        else { return Ok(Json(res)); }
    }
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] string email, [FromForm] string passward, string name)
    {
        UserDTO user = new() { Email = email, Password = passward, Name = name };
        var res = await _userServices.Register(user);
        if (res == null) { return BadRequest("Invalid email"); }
        return Ok(res);
    }

}
