using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement_DB;
using UserManagement_DB.Models;
using UserManagement_Repositories.Dtos;
using UserManagement_Repositories.Exceptions;

namespace UserManagement_Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> RegisterUserProfile(RegisterUserProfileDto userRegistration);
        Task<List<UserProfile>> getUserProfileById(int Id);
        Task<List<UserProfile>> getUsersProfileById();
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


        public async Task<List<UserProfile>> getUserProfileById(int Id)
        {
            var userProfile = await _context.UserProfiles
                             .Include(x=>x.User)
                             .Where(x => x.Id == Id).ToListAsync();

            if(userProfile.Count == 0)
            {
                throw new MyExceptions("User Profile Cannot Found");
            }
            

            return userProfile;
        }


        public async Task<List<UserProfile>> getUsersProfileById()
        {
            var userProfile = await _context.UserProfiles
                            .Include(x => x.User)
                            .ToListAsync();

            if (userProfile.Count == 0)
            {
                throw new MyExceptions("Users Profile Cannot Found");
            }


            return userProfile;
        }


    }


}
