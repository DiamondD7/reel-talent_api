using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reel_talent_api.Application.Interfaces.Services;
using reel_talent_api.Domain.Models.DTO;
using reel_talent_api.Domain.Models.DTO.Requests;
using reel_talent_api.Domain.Models.DTO.Results;

namespace reel_talent_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost("signin"), AllowAnonymous]
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
        public async Task<IActionResult> AddTalentUser(SignupRequest req)
        {
            var userCreation = await _authService.SignupAsync(req);
            if(userCreation == null)
            {
                return BadRequest("email is already taken");
            }

            return CreatedAtAction(nameof(GetUserByLoginCred), userCreation);
        }

        [HttpPost("logout"), AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            if (Request.Cookies.TryGetValue("refreshToken", out var raw))
                await _authService.LogoutAsync(raw);   // revoke that token

            ClearAuthCookies();
            return NoContent();
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleSignupValidation([FromBody]GoogleSignupRequest req)
        {
            var result = await _authService.GoogleSignupAsync(req);
            if(result == null)
            {
                return Unauthorized();
            }

            return Ok(result);


        }


        [HttpPost("refresh"), AllowAnonymous]
        public async Task<IActionResult> RefreshInvalidToken()
        {
            if(!Request.Cookies.TryGetValue("refreshToken", out var rawToken) || string.IsNullOrWhiteSpace(rawToken))
            {
                return Unauthorized();
            }

            var result = await _authService.RefreshTokenAsync(rawToken);
            if(result == null)
            {

                ClearAuthCookies();
                return Unauthorized();
            }

            SetAuthCookies(result.Tokens);

            return Ok(result.User);
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
                Path = "/api/auth",
                Expires = tokens.RefreshTokenExpiresAt
            });
        }


        private void ClearAuthCookies()
        {
            Response.Cookies.Delete("accessToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            Response.Cookies.Delete("refreshToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/api/auth"
            });
        }
    }
}
