using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSEducation.AuthenticationManager.Models;
using MSEducation.AuthenticationManager.Services;


namespace MSEducation.Gateaway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JWTService jwtService;
        private readonly SignInManager<IdentityUser> signInManager;
        public AuthController(JWTService _jwtService, SignInManager<IdentityUser> _signInManager)
        {
            jwtService = _jwtService;
            signInManager = _signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await signInManager.UserManager.FindByNameAsync(model.UserName);

            if (user == null)
                return NotFound();

            var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!passwordCheck.Succeeded)
                return BadRequest();

            var token = await jwtService.GenerateJwtTokenAsync(user);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!model.Validate())
                return BadRequest();

            var user = new IdentityUser { UserName = model.UserName, Email = model.Email };

            var creation = await signInManager.UserManager.CreateAsync(user, model.Password);

            if (!creation.Succeeded)
                return BadRequest(creation.Errors);

            var token = await jwtService.GenerateJwtTokenAsync(user);
            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        [Route("test")]
        public IActionResult TestMe()
        {
            return Ok("I'am working");
        }
    }
}
