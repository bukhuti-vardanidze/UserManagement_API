using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement_DB.Models;
using UserManagement_Repositories;
using UserManagement_Repositories.Dtos;

namespace UserManagement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterUserProfile([FromQuery] RegisterUserProfileDto registerUserProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registeredProfile = await _userProfileRepository.RegisterUserProfile(registerUserProfile);

            if (registeredProfile == null)
            {
                
                return BadRequest("Failed to register user profile.");
            }

            return Ok(registeredProfile);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userProfile = await _userProfileRepository.getUserProfileById(id);
            
            if (userProfile == null)
            {
                return NotFound($"User Profile with Id: {id} not found.");
            }

            return Ok(userProfile);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersProfile()
        {
            var userProfiles = await _userProfileRepository.getUsersProfileById();

            if (userProfiles == null || userProfiles.Count == 0)
            {
                return NotFound("No user profiles found.");
            }

            return Ok(userProfiles);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromBody]UpdateUserProfileDto updateUserProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProfile = await _userProfileRepository.UpdateUserProfile(updateUserProfile);

            if (updatedProfile == null)
            {
                return BadRequest("Failed to update user profile.");
            }

            return Ok(updatedProfile);
        }

        [HttpDelete("personalnumber")]
        public async Task<IActionResult> DeleteUserProfile(string personalnumber)
        {

            var deletedProfile = await _userProfileRepository.DeleteUserProfile(personalnumber);

            if (deletedProfile == null)
            {
                return NotFound("User profile not found OR already Removed.");
            }

            return Ok(deletedProfile);
        }

    }
}
