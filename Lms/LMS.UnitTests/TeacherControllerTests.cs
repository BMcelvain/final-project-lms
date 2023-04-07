using FluentAssertions;
using Lms.APIErrorHandling;
using Lms.Controllers;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.UnitTests
{
    #nullable disable warnings
    [TestClass] 
    public class TeacherControllerTests
    {
        Mock<ITeacherDao> mockTeacherDao;
        TeacherController sut;
        Guid teacherGuid;
        JsonPatchDocument<TeacherModel> teacherJsonDocument;
        List<TeacherModel> teachers;

        [TestInitialize]
        public void Initialize()
        {
            mockTeacherDao = new Mock<ITeacherDao>();
            sut = new TeacherController(mockTeacherDao.Object);
            teacherGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6");
            teacherJsonDocument = new JsonPatchDocument<TeacherModel>();
         
            teachers = new List<TeacherModel>()
            {
                new TeacherModel()
                {
                    TeacherId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6"),
                    TeacherFirstName = "Test",
                    TeacherLastName = "Teacher",
                    TeacherPhone = "999-999-9999",
                    TeacherEmail = "teacher@vu.com",
                    TeacherStatus = "Active"
                },
                new TeacherModel()
                {
                    TeacherId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A7"),
                    TeacherFirstName = "Tester",
                    TeacherLastName = "Substitute",
                    TeacherPhone = "999-999-9998",
                    TeacherEmail = "subTeacher@vu.com",
                    TeacherStatus = "Active"
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            mockTeacherDao = null;
            sut = null;
            teacherGuid = new Guid();
            teacherJsonDocument = null;
            teachers = null;
        }

        [TestMethod]
        public async Task CreateTeacher_ReturnsOkResponse_WhenModelIsValid()
        {
            // Act
            var result = await sut.CreateTeacher(teachers.First());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public async Task GetTeachersById_ReturnsOkResponse_WhenGuidIsValid()
        {
            // Arrange
            mockTeacherDao
                .Setup(x => x.GetTeacherById(teacherGuid))
                .ReturnsAsync(teachers.First());

            // Act
            var result = await sut.GetTeacherById(teacherGuid);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var teacherInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            teacherInApiOkResponse.Should().NotBeNull();
            teacherInApiOkResponse.Should().BeEquivalentTo(teachers.First());
        }

        [TestMethod]
        public async Task GetTeacherById_ReturnsNotFoundResponse_WhenGuidIsInvalid()
        {
            // Act
            var result = await sut.GetTeacherById(teacherGuid);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Teacher with that id not found.");
        }

        [TestMethod]
        public async Task GetTeacherByStatus_ReturnsOkResponse_WhenStatusIsActive()
        {
            // Arrange
            mockTeacherDao
                .Setup(x => x.GetTeacherByStatus("Active"))
                .ReturnsAsync(teachers);

            // Act
            var result = await sut.GetTeacherByStatus("Active");

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var teachersInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            teachersInApiOkResponse.Should().BeEquivalentTo(teachers);
        }

        [TestMethod]
        public async Task GetTeacherByStatus_ReturnsBadRequestResponse_WhenStatusIsNotActiveOrInactive()
        {
            // Act
            var result = await sut.GetTeacherByStatus("Test");

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            var apiResponseInBadRequestResult = badRequestResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            apiResponseInBadRequestResult.StatusCode.Should().Be(400);
            apiResponseInBadRequestResult.Message.Should().BeEquivalentTo("Please enter Active or Inactive status.");
        }

        [TestMethod]
        public async Task GetTeacherByStatus_ReturnsNotFoundResponse_WhenTeacherWithStatusNotFound()
        {
            // Act
            var result = await sut.GetTeacherByStatus("Inactive");

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInBadRequestResult = notFoundResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInBadRequestResult.StatusCode.Should().Be(404);
            apiResponseInBadRequestResult.Message.Should().BeEquivalentTo("Teacher with status Inactive not found.");
        }

        [TestMethod]
        public async Task PartiallyUpdateTeacherById_ReturnsOkResponse_WhenGuidIsValid()
        {
            // Arrange
            mockTeacherDao
                .Setup(x => x.GetTeacherById(teacherGuid))
                .ReturnsAsync(teachers.First());

            // Act
            var result = await sut.PartiallyUpdateTeacherById(teacherGuid, teacherJsonDocument);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var teacherInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            teacherInApiOkResponse.Should().NotBeNull();
            teacherInApiOkResponse.Should().BeEquivalentTo(teachers.First());
        }

        [TestMethod]
        public async Task PartiallyUpdateTeacherById_ReturnsNotFoundResponse_WhenGuidIsInvalid()
        {
            // Act
            var result = await sut.PartiallyUpdateTeacherById(teacherGuid, teacherJsonDocument);

            // Arrange
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Teacher with that id not found.");
        }

        [TestMethod]
        public async Task DeleteTeacherById_ReturnsOkResponse_WhenGuidIsValid()
        {
            // Arrange
            mockTeacherDao
                .Setup(x => x.GetTeacherById(teacherGuid))
                .ReturnsAsync(teachers.First());

            // Act
            var result = await sut.DeleteTeacherById(teacherGuid);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var teacherInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            teacherInApiOkResponse.Should().NotBeNull();
            teacherInApiOkResponse.Should().BeEquivalentTo(teachers.First());
        }

        [TestMethod]
        public async Task DeleteTeacherById_ReturnsNotFound_WhenGuidIsInvalid()
        {
            // Act
            var result = await sut.DeleteTeacherById(teacherGuid);

            // Assert 
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Teacher with that id not found.");
        }
    }
}