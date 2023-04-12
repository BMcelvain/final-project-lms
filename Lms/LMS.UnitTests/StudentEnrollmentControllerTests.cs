using Lms.Controllers;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Lms.APIErrorHandling;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;

namespace LMS.UnitTests
{
    #nullable disable warnings
    [TestClass]
    public class StudentEnrollmentControllerTests
    {
        Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao;
        StudentEnrollmentController sut;
        Guid testGuid;
        Guid invalidtestGuid;
        JsonPatchDocument<StudentEnrollmentModel> studentEnrollmentJsonDocument;
        List<StudentEnrollmentModel> studentEnrollment;
        IMemoryCache cache;

        [TestInitialize]
        public void Initialize()
        {
            mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            cache = new MemoryCache(new MemoryCacheOptions());
            sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object, cache);
            testGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9");
            invalidtestGuid = new Guid("00000000-0000-0000-0000-000000000000");
            studentEnrollmentJsonDocument = new JsonPatchDocument<StudentEnrollmentModel>();

            studentEnrollment = new List<StudentEnrollmentModel>()
            {
                new StudentEnrollmentModel()
                {
                    CourseId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B2"),
                    CourseName = "Unit Testing",
                    StartDate = "2023-03-16",
                    EndDate = "2023-5-16",
                    Cancelled = false,
                    CancellationReason = "Test Cancelled",
                    HasPassed = false
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            mockStudentEnrollmentDao = null;
            sut = null;
            testGuid = new Guid();
            studentEnrollmentJsonDocument = null;
            studentEnrollment = null;
        }

        [TestMethod]
        public async Task GetStudentEnrollmentHistoryByStudentId_ValidGuid_ReturnsOKStatusCode()
        {
            // Arrange
            mockStudentEnrollmentDao
                .Setup(x => x.GetStudentEnrollmentHistoryByStudentId(testGuid))
                .ReturnsAsync(studentEnrollment);

            // Act
            var result = await sut.GetStudentEnrollmentHistoryByStudentId(testGuid);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var studentInApiOkResponse = apiOkResponseInOkResult.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            studentInApiOkResponse.Should().NotBeNull();
            studentInApiOkResponse.Should().BeEquivalentTo(studentEnrollment);
        }

        [TestMethod]
        public async Task GetStudentEnrollmentHistoryByStudentId_InvalidGuid_ReturnsNotFoundResponse()
        {
            // Act
            var result = await sut.GetStudentEnrollmentHistoryByStudentId(testGuid);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Student enrollment with that Student Id not found.");
        }

        [TestMethod]
        public async Task GetStudentEnrollmentHistoryByStudentLasttName_ValidLastName_ReturnsOKStatusCode()
        {
            // Arrange
            mockStudentEnrollmentDao
                .Setup(x => x.GetStudentEnrollmentHistoryByStudentLastName("Test"))
                .ReturnsAsync(studentEnrollment);

            // Act
            var result = await sut.GetStudentEnrollmentHistoryByStudentLastName("Test");

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var studentInApiOkResponse = apiOkResponseInOkResult.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            studentInApiOkResponse.Should().NotBeNull();
            studentInApiOkResponse.Should().BeEquivalentTo(studentEnrollment);
        }

        [TestMethod]
        public async Task GetStudentEnrollmentByStudentLastName_InvalidLastName_ThrowsExceptionOnError()
        {
            // Act
            var result = await sut.GetStudentEnrollmentHistoryByStudentLastName("Test33");

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Student enrollment for student with that last name not found.");
        }

        [TestMethod]
        public async Task GetActiveStudentEnrollmentByStudentPhone_ValidPhone_ReturnsOKStatusCode()
        {
            // Arrange
            string validPhoneNumber = "123-456-7890";
            mockStudentEnrollmentDao
                .Setup(x => x.GetActiveStudentEnrollmentByStudentPhone(validPhoneNumber))
                .ReturnsAsync(studentEnrollment);

            // Act
            var result = await sut.GetActiveStudentEnrollmentByStudentPhone(validPhoneNumber);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var studentEnrollmentInApiOkResponse = apiOkResponseInOkResult.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            studentEnrollmentInApiOkResponse.Should().NotBeNull();
            studentEnrollmentInApiOkResponse.Should().BeEquivalentTo(studentEnrollment);
        }

        [TestMethod]
        public async Task GetActiveStudentEnrollmentByStudentPhone_InvalidPhone_ThrowsExceptionOnError()
        {
            // Arrange
            string invalidPhoneNumber = "123-777-222";

            // Act
            var result = await sut.GetActiveStudentEnrollmentByStudentPhone(invalidPhoneNumber);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Student enrollment for student with that phone number not found.");
        }
    }
}
