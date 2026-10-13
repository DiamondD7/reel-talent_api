using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reel_talent_api.Application.Interfaces.Repositories;
using reel_talent_api.Application.Interfaces.Services;
using reel_talent_api.Domain.Models.DTO;
using reel_talent_api.Domain.Models.DTO.Requests;

namespace reel_talent_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> GetUserByLoginCred([FromBody] LoginRequest req)
        {
            var talent = await _authService.LoginAsync(req);
            if (talent == null)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(talent);
        }

        [HttpPost("signup")]
        public async Task<IActionResult>AddTalentUser(SignupRequest req)
        {
            var userCreation = await _authService.SignupAsync(req);

            return CreatedAtAction(nameof(GetUserByLoginCred), userCreation);
        }
    }
}
