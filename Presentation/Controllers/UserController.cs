﻿using Core.DTO;
using Core.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController(UserServices userServices, IConfiguration configuration) : Controller
{
    private readonly UserServices _userServices = userServices;
    private readonly IConfiguration _configuration = configuration;
    [HttpPost]
    public async Task<IActionResult> LogIn([FromBody]UserDTO userDTO)
    {
        var res = await _userServices.LogIn(userDTO);
        if (res == null || string.IsNullOrEmpty(res)) { return BadRequest("ERROR"); }
        
        else { return Ok(new TokenDTO { Token = res} ) ; }
    }
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
    {        
        var res = await _userServices.Register(userDTO);
        if (res == null) { return BadRequest("Invalid email"); }
        return Ok(res);
    }

}
