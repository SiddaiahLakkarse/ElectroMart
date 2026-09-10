using ElectroMart.API.DTOs; using ElectroMart.API.Services; using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc; using System.Security.Claims;
namespace ElectroMart.API.Controllers;
[ApiController,Route("api/auth")] public class AuthController(CommerceService s):ControllerBase{
 [HttpPost("register")] public async Task<IActionResult> Register(RegisterRequest r)=>Ok(await s.Register(r));
 [HttpPost("login")] public async Task<IActionResult> Login(LoginRequest r)=> (await s.Login(r)) is { } a?Ok(a):Unauthorized(new{error="Invalid email or password."});
 [Authorize,HttpGet("me")] public async Task<IActionResult> Me(){var u=await s.Orders(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!));return Ok(new{email=User.FindFirstValue(ClaimTypes.Email),role=User.FindFirstValue(ClaimTypes.Role)});}
}
