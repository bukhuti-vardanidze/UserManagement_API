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
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest( );
            }
            if (await _authRepository.Login(user))
            {
                var tokenString = _authRepository.GenerateTokenString(user);
                return Ok(tokenString);
            }
            return Unauthorized("Ivalid Email or Password!");
        }

       
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUseeeer([FromQuery] RegisterUserDto user)
        {
            if (await _authRepository.Register(user))
            {
                return Ok("Successfuly done");
            }
            return BadRequest("Something went worng");
        }
    }
}
