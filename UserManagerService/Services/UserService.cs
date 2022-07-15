using AutoMapper;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using UserManagerService.Dtos;
using UserManagerService.Models;
using UserManagerService.Repositories.Interfaces;
using UserManagerService.Services.Interfaces;

namespace UserManagerService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Return all active users from the database
        /// </summary>
        /// <returns>List of active users</returns>
        public Response<List<User>> GetAllActiveUsers()
        {
            var response = new Response<List<User>>();

            try
            {
                _logger.LogInformation("UserService - Getting all active users");
                var result = _userRepository.GetAllActiveUsers();
                if (result != null)
                {
                    response.Data = result;
                    response.IsSuccess = true;
                    response.Message = $"{result.Count} user/s were found.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("UserService - The following error occurs: {@ex}", ex);
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Insert a new User in the database
        /// </summary>
        /// <param name="user">User to insert</param>
        /// <returns>Object with information</returns>
        public Response<object> AddUser(UserDto user)
        {
            var response = new Response<object>();

            try
            {
                _logger.LogInformation("UserService - Adding new user");
                var userToAdd = _mapper.Map<User>(user);
                _userRepository.AddUser(userToAdd);

                response.Data = user;
                response.IsSuccess = true;
                response.Message = $"{userToAdd.Name} was added successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogInformation("UserService - The following error occurs: {@ex}", ex);
            }

            return response;
        }

        /// <summary>
        ///  Update the Active property of a given user
        /// </summary>
        /// <param name="userId">Id of the user to update</param>
        /// <param name="active">State of the user</param>
        /// <returns>Object with information</returns>
        public Response<object> UpdateUserState(int userId, bool active)
        {
            var response = new Response<object>();

            try
            {
                _logger.LogInformation("UserService - Updating user state");
                var result = _userRepository.UpdateUserState(userId, active);
                if (result)
                {
                    response.Data = userId;
                    response.IsSuccess = true;
                    response.Message = $" User with Id: {userId} updated successfully.";
                }
                else
                {
                    response.Message = $" User with Id: {userId} could not be updated.";
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("UserService - The following error occurs: {@ex}.", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a user from the database
        /// </summary>
        /// <param name="userId">Id of the user to delete</param>
        /// <returns>Object with information</returns>
        public Response<object> DeleteUser(int userId)
        {
            var response = new Response<object>();

            try
            {
                _logger.LogInformation("UserService - Deleting user");
                var result = _userRepository.DeleteUser(userId);

                if (result)
                {
                    response.Data = userId;
                    response.IsSuccess = true;
                    response.Message = $" User with Id: {userId} deleted successfully.";
                }
                else
                {
                    response.Message = $" User with Id: {userId} could not be deleted.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("UserService - The following error occurs: {@ex}", ex);
            }

            return response;
        }
    }
}
