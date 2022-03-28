using Abstraction.Interfaces.Services;
using Abstraction.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IPlayerService _playerService;
        public AuthController(IAuthService authService, IPlayerService playerService)
        {
            _authService = authService;
            _playerService = playerService;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SigninRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var token = await _authService.SignIn(request.Username, request.Password);

            if (token != null)
            {
                return Ok(new { accessToken = token });
            }
            else
                return Unauthorized(new { Message = "UnAuthorized" });
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _playerService.GetAsync(request.Username);
            if (user != null)
            {
                return BadRequest("username already existed");
            }
            var newPlayerToken = await _authService.SignUp(
                new SignupRequest
                {
                    Username = request.Username,
                    Password = request.Password
                });
            return Ok(new { accessToken = newPlayerToken });
        }
    }
}
