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
        Task<List<UserProfile>> getUserProfileById(int ID);
        Task<List<UserProfile>> getUsersProfileById();
        Task<UserProfile> UpdateUserProfile(UpdateUserProfileDto updateUserProfile);
        Task<UserProfile> DeleteUserProfile(string personalNumber);
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

                _context.UserProfiles.Add(registerUserProfile);
                await _context.SaveChangesAsync();

                return registerUserProfile;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }


        public async Task<List<UserProfile>> getUserProfileById(int ID)
        {

            try
            {
                 var userProfile = await _context.UserProfiles
                             .Include(x=>x.User)
                             .Where(x => x.Id == ID).ToListAsync();

            if(userProfile.Count == 0)
            {
                throw new MyExceptions("User Profile Cannot Found");
            }
            

            return userProfile;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }


        public async Task<List<UserProfile>> getUsersProfileById()
        {
            
            try
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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<UserProfile> UpdateUserProfile(UpdateUserProfileDto updateUserProfile)
        {
 
            try
            {
                var updateProfile = await _context.UserProfiles
                         .Include(x=>x.User)
                         .Where(x => x.PersonalNumber.Contains(updateUserProfile.PersonalNumber))
                         .FirstOrDefaultAsync();

                if (updateProfile != null)
                {
                    updateProfile.FirstName = updateUserProfile.FirstName;
                    updateProfile.LastName = updateUserProfile.LastName;
                    updateProfile.PersonalNumber = updateUserProfile.PersonalNumber;
                    updateProfile.UserId = updateUserProfile.UserId;
                }
                _context.UserProfiles.Update(updateProfile);
                await _context.SaveChangesAsync();

                return updateProfile;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }


        public async Task<UserProfile> DeleteUserProfile(string personalNumber)
        {
            try 
            {
                var deleteUserProfile = await _context.UserProfiles
                    .Include(x=>x.User)
                    .Where(x=>x.PersonalNumber == personalNumber)
                    .FirstOrDefaultAsync();

                if(deleteUserProfile == null)
                {
                    throw new Exception("User Profile Not Found!");   
                }

                _context.UserProfiles.Remove(deleteUserProfile);
                await _context.SaveChangesAsync();


                return deleteUserProfile;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
             }
        }

    }


}
