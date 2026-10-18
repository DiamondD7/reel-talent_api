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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserDetails()
        {
            return Ok("here");
        }

        

        
    }
}
