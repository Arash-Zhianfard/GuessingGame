using Abstraction.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filter;
using WebApp.Models;

namespace WebApp.Controllers
{
    [AuthorizeFilterAttribute]
    public class GameController : BaseGameController
    {
        private readonly IGameCoreService _gameCoreService;
        public GameController(IGameCoreService gameCoreService, IAuthService authService) : base(authService)
        {
            _gameCoreService = gameCoreService;
        }
        [HttpPost("Play")]
        public async Task<IActionResult> Play(GameRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var gameResult=await _gameCoreService.Play(UserId, request.Number, request.Point);
            return Ok(gameResult);
        }
    }
}
