using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement_DB;
using UserManagement_DB.Models;
using UserManagement_Repositories.Dtos;

namespace UserManagement_Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> RegisterUserProfile(RegisterUserProfileDto userRegistration);
    }

    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserManagementContext _context;

        public UserProfileRepository(UserManagementContext context)
        {
            _context = context;
        }
    
        public async Task<UserProfile> RegisterUserProfile(RegisterUserProfileDto userRegistration)
        {
            try
            {
                var registerUserProfile = new UserProfile
                {
                    UserId = userRegistration.UserId,
                    FirstName = userRegistration.FirstName,
                    LastName = userRegistration.LastName,
                    PersonalNumber = userRegistration.PersonalNumber
                };

                _context.UserProfiles.AddAsync(registerUserProfile);
                await _context.SaveChangesAsync();

                return registerUserProfile;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    
    }


}
