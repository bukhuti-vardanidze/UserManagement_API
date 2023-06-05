using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement_DB.Models;
using UserManagement_Repositories;
using UserManagement_Repositories.Dtos;

namespace UserManagement_API.Controllers
{
    [Route("api/")]
    [ApiController]
   // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterUserProfile([FromQuery] RegisterUserProfileDto registerUserProfile)
        {
            var register = await _userProfileRepository.RegisterUserProfile(registerUserProfile);
            if(register == null)
            {
                return BadRequest();
            }
            return Ok(register);
        }
    }
}
