using FluentAssertions;
using Lms.APIErrorHandling;
using Lms.Controllers;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Lms.APIErrorHandling;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System;

namespace LMS.UnitTests
{
    #nullable disable warnings
    [TestClass]
    public class CourseControllerTests
    {
        Mock<ICourseDao> mockCourseDao;
        CourseController sut;
        Guid courseGuid;
        Guid studentGuid;
        JsonPatchDocument<CourseModel> courseJsonDocument;
        JsonPatchDocument<StudentInCourseModel> studentInCourseJsonDocument;
        List<CourseModel> courses;
        StudentInCourseModel studentInCourse;
        IMemoryCache cache;

        [TestInitialize]
        public void Initialize()
        {
            mockCourseDao = new Mock<ICourseDao>();
            cache = new MemoryCache(new MemoryCacheOptions());
            sut = new CourseController(mockCourseDao.Object, cache);
            courseGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6");
            studentGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B2");
            courseJsonDocument  = new JsonPatchDocument<CourseModel>();
            studentInCourseJsonDocument = new JsonPatchDocument<StudentInCourseModel>();
            courses = new List<CourseModel>()
            {
                new CourseModel()
                {
                    CourseId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6"),
                    TeacherId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A7"),
                    CourseName = "Test",
                    StartDate = "01/01/2023",
                    EndDate = "03/01/2023",
                    CourseStatus = "Active"
                },
                new CourseModel()
                {
                    CourseId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A8"),
                    TeacherId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9"),
                    CourseName = "Test",
                    StartDate = "01/01/2023",
                    EndDate = "03/01/2023",
                    CourseStatus = "Active"
                }
            };
            studentInCourse = new StudentInCourseModel()
            {
                StudentId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B0"),
                CourseId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B1"),
                Cancelled = false,
                CancellationReason = null,
                HasPassed= false
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            mockCourseDao = new Mock<ICourseDao>();
            sut = null;
            courseGuid = new Guid();
            studentGuid = new Guid();
            courseJsonDocument = null;
            studentInCourseJsonDocument = null; 
            courses = null;
            studentInCourse = null;
        }

        [TestMethod]
        public async Task CreateCourse_ReturnsOkResponse_WhenModelIsValid()
        { 
            // Arrange
            mockCourseDao
                .Setup(x => x.CreateCourse(It.IsAny<CourseModel>()))
                .Callback(() => { return; });

            // Act
            var result = await sut.CreateCourse(courses.First());

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var courseInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            courseInApiOkResponse.Should().NotBeNull();
            courseInApiOkResponse.Should().BeEquivalentTo(courses.First());
        }

        [TestMethod]
        public async Task GetCoursesById_ReturnsCourseAndOkResponse_WhenGuidIsValid()
        {
            // Arrange
            mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(courseGuid))
                .ReturnsAsync(courses.First());

            // Act
            var result = await sut.GetCourseById(courseGuid);
            
            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var courseInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            courseInApiOkResponse.Should().NotBeNull();
            courseInApiOkResponse.Should().BeEquivalentTo(courses.First());
        }

        [TestMethod]
        public async Task GetCourseById_ReturnsNotFoundResponse_WhenGuidIsInvalid()
        {
            // Act
            var result = await sut.GetCourseById(courseGuid);
            
            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Course with that id not found.");
        }

        [TestMethod]
        public async Task GetCourseByStatus_ReturnsCoursesAndOkResponse_WhenStatusIsActive()
        {
            // Arrange
            mockCourseDao
                .Setup(x => x.GetCourseByStatus("Active"))
                .ReturnsAsync(courses);

            // Act
            var result = await sut.GetCourseByStatus("Active");

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var coursesInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            coursesInApiOkResponse.Should().BeEquivalentTo(courses);
        }

        [TestMethod]
        public async Task GetCourseByStatus_ReturnsBadRequestResponse_WhenStatusIsNotActiveOrInactive()
        {
            // Act
            var result = await sut.GetCourseByStatus("Test");

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            var apiResponseInBadRequestResult = badRequestResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            apiResponseInBadRequestResult.StatusCode.Should().Be(400);
            apiResponseInBadRequestResult.Message.Should().BeEquivalentTo("Please enter Active or Inactive status.");
        }

        [TestMethod]
        public async Task PartiallyUpdateCourseById_ReturnsCourseAndOkResponse_WhenGuidIsValid()
        {
            // Arrange
            mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(courseGuid))
                .ReturnsAsync(courses.First());

            // Act
            var result = await sut.PartiallyUpdateCourseById(courseGuid, courseJsonDocument);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var courseInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            courseInApiOkResponse.Should().NotBeNull();
            courseInApiOkResponse.Should().BeEquivalentTo(courses.First());
        }

        [TestMethod]
        public async Task PartiallyUpdateCourseById_ReturnsNotFound_WhenGuidIsInvalid()
        {
            // Act
            var result = await sut.PartiallyUpdateCourseById(courseGuid, courseJsonDocument);

            // Arrange
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Course with that id not found.");
        }

        [TestMethod]
        public async Task DeleteCourseById_ReturnsCourseAndOkResponse_WhenGuidIsValid()
        {
            // Arrange
            mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(courseGuid))
                .ReturnsAsync(courses.First());

            // Act
            var result = await sut.DeleteCourseById(courseGuid);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var courseInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            courseInApiOkResponse.Should().NotBeNull();
            courseInApiOkResponse.Should().BeEquivalentTo(courses.First());
        }

        [TestMethod]
        public async Task DeleteCourseById_ReturnsNotFoundResponse_WhenCourseGuidIsInvalid()
        {
            // Act
            var result = await sut.DeleteCourseById(courseGuid);

            // Assert 
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Course with that id not found.");
        }

        //---------------Add Student To Course Section-------------- 

        [TestMethod]
        public async Task StudentInCourse_ReturnsOkResponse_WhenStudentInCourseModelIsValid()
        {
            // Arrange 
            mockCourseDao
                .Setup(x => x.StudentInCourse(It.IsAny<StudentInCourseModel>()))
                .Callback(() => { return; });

            // Act
            var result = await sut.StudentInCourse(studentInCourse);

            //Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var studentInCourseWithinApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            studentInCourseWithinApiOkResponse.Should().NotBeNull();
            studentInCourseWithinApiOkResponse.Should().BeEquivalentTo(studentInCourse);
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentInCourseById_ReturnsStudentInCourseAndOkResponse_WhenStudentCourseGuidsAndJsonDocAreValid()
        {
            //Arrange
            mockCourseDao
                .Setup(x => x.GetCourseById<StudentInCourseModel>(courseGuid))
                .ReturnsAsync(studentInCourse);

            //Act
            var result = await sut.PartiallyUpdateStudentInCourseByCourseStudentId(studentGuid, courseGuid, studentInCourseJsonDocument);

            //Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var studentInCourseWithinApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            studentInCourseWithinApiOkResponse.Should().NotBeNull();
            studentInCourseWithinApiOkResponse.Should().BeEquivalentTo(studentInCourse);
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentInCourseByCourseStudentId_ReturnsNotFoundResponse_WhenCourseGuidIsInvalid()
        {
            // Act
            var result = await sut.PartiallyUpdateStudentInCourseByCourseStudentId(studentGuid, courseGuid, studentInCourseJsonDocument);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Course with that id not found.");
        }

        [TestMethod]
        public async Task DeleteStudentInCourseByStudentCourseId_ReturnsStudentInCourseAndOkResponse_WhenStudentAndCourseGuidsAreValid()
        {
            // Arrange
            mockCourseDao
                .Setup(x => x.GetCourseById<StudentInCourseModel>(courseGuid))
                .ReturnsAsync(studentInCourse);

            // Act
            var result = await sut.DeleteStudentInCourseByStudentCourseId(studentGuid, courseGuid);

            //Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var studentInCourseWithinApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            studentInCourseWithinApiOkResponse.Should().NotBeNull();
            studentInCourseWithinApiOkResponse.Should().BeEquivalentTo(studentInCourse);
        }

        [TestMethod]
        public async Task DeleteStudentInCourseByIdId_ReturnsNotFoundResponse_WhenCourseGuidIsInvalid()
        {
            // Act
            var result = await sut.DeleteStudentInCourseByStudentCourseId(studentGuid, courseGuid);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Course with that id not found.");
        }
    }
}