using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reel_talent_api.Application.Interfaces.Repositories;
using reel_talent_api.Application.Interfaces.Services;
using reel_talent_api.Domain.Models;
using reel_talent_api.Domain.Models.DTO;
using reel_talent_api.Domain.Models.DTO.Requests;
using reel_talent_api.Domain.Models.DTO.Results;

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
            var result = await _authService.LoginAsync(req);
            if (result == null)
            {
                return Unauthorized("Invalid email or password");
            }

            SetAuthCookies(result.Tokens);

            return Ok(result.User);
        }

        [HttpPost("signup")]
        public async Task<IActionResult>AddTalentUser(SignupRequest req)
        {
            var userCreation = await _authService.SignupAsync(req);

            return CreatedAtAction(nameof(GetUserByLoginCred), userCreation);
        }

        private void SetAuthCookies(TokenResult tokens)
        {
            Response.Cookies.Append("accessToken", tokens.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = tokens.AccessTokenExpiresAt
            });

            Response.Cookies.Append("refreshToken", tokens.RawRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/api/auth/refresh",
                Expires = tokens.RefreshTokenExpiresAt
            });
        }
    }
}
