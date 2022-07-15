using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UserManagerService.Controllers;
using UserManagerService.Dtos;
using UserManagerService.Models;
using UserManagerService.Services.Interfaces;

namespace UserManagerService_Test.Repositories
{
    public class UserControllerTest
    {
        UserController target;
        Mock<IUserService> _mockUserService;
        public UserControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            target = new UserController(_mockUserService.Object);
        }

        [Test]
        public void GetAllActiveUsers_ShouldReturnOkStatusCode_When_IsSuccessIsTrue()
        {
            //Arrange
            var listOfActiveUsers = CreateResponseWithListOfActiveUsers();
            listOfActiveUsers.IsSuccess = true;
            _mockUserService
                .Setup(m => m.GetAllActiveUsers()).Returns(listOfActiveUsers);

            //Act
            var res = target.GetAllActiveUsers();
            var OkResult = res as OkObjectResult; //Try to convert to type, if cannot convert return null.

            //Assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);
        }

        [Test]
        public void AddUser_ShouldReturnBadRequestStatusCode_When_IsSuccessIsFalse()
        {
            //Arrange
            var userDto = CreateUserDto();
            var response = new Response<object>();
            response.IsSuccess = false;

            _mockUserService
                .Setup(m => m.AddUser(userDto)).Returns(response);

            //Act
            var res = target.AddUser(userDto);
            var badRequestResult = res as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void AddUser_ShouldReturnOkStatusCode_When_IsSuccessIsTrue()
        {
            //Arrange
            var userDto = CreateUserDto();
            var response = new Response<object>();
            response.IsSuccess = true;

            _mockUserService
                .Setup(m => m.AddUser(userDto)).Returns(response);

            //Act
            var res = target.AddUser(userDto);
            var OkResult = res as OkObjectResult;

            //Assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);
        }

        [Test]
        public void UpdateUserState_ShouldReturnBadRequestStatusCode_When_IsSuccessIsFalse()
        {
            //Arrange
            var response = new Response<object>();
            response.IsSuccess = false;

            _mockUserService
                .Setup(m => m.UpdateUserState(1,true)).Returns(response);

            //Act
            var res = target.UpdateUserState(1,true);
            var badRequestResult = res as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void UpdateUserState_ShouldReturnOkStatusCode_When_IsSuccessIsTrue()
        {
            //Arrange
            var userDto = CreateUserDto();
            var response = new Response<object>();
            response.IsSuccess = true;

            _mockUserService
                .Setup(m => m.UpdateUserState(1, true)).Returns(response);

            //Act
            var res = target.UpdateUserState(1, true);
            var OkResult = res as OkObjectResult;

            //Assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);
        }

        [Test]
        public void DeleteUser_ShouldReturnBadRequestStatusCode_When_IsSuccessIsFalse()
        {
            //Arrange
            var response = new Response<object>();
            response.IsSuccess = false;

            _mockUserService
                .Setup(m => m.DeleteUser(1)).Returns(response);

            //Act
            var res = target.DeleteUser(1);
            var badRequestResult = res as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void DeleteUser_ShouldReturnOkStatusCode_When_IsSuccessIsTrue()
        {
            //Arrange
            var userDto = CreateUserDto();
            var response = new Response<object>();
            response.IsSuccess = true;

            _mockUserService
                .Setup(m => m.DeleteUser(1)).Returns(response);

            //Act
            var res = target.DeleteUser(1);
            var OkResult = res as OkObjectResult;

            //Assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);
        }

        private Response<List<User>> CreateResponseWithListOfActiveUsers()
        {
            var data = new List<User>();
            var response = new Response<List<User>>();

            data.Add(new User { Id = 1, Name = "Puli", Active = true, BirthDate = DateTime.Now });
            data.Add(new User { Id = 2, Name = "Bo", Active = true, BirthDate = DateTime.Now });
            data.Add(new User { Id = 3, Name = "Poco", Active = true, BirthDate = DateTime.Now });

            response.Data = data;

            return response;
        }

        private UserDto CreateUserDto()
        {
            return new UserDto() { BirthDate = DateTime.Now, Name = "MetalGear" };
        }
    }
}
