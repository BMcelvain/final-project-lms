using FluentAssertions;
using Lms.APIErrorHandling;
using Lms.Controllers;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;


namespace LMS.UnitTests
{
    #nullable disable warnings
    [TestClass]
    public class CourseControllerTests
    {
        private Mock<ICourseDao> _mockCourseDao;
        private CourseController _sut;
        private Guid _courseGuid;
        private Guid _studentGuid;
        private JsonPatchDocument<CourseModel> _courseJsonDocument;
        private List<CourseModel> _courses;
        private IMemoryCache _cache;

        [TestInitialize]
        public void Initialize()
        {
            _mockCourseDao = new Mock<ICourseDao>();
            _cache = new MemoryCache(new MemoryCacheOptions());
            _sut = new CourseController(_mockCourseDao.Object, _cache);
            _courseGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6");
            _studentGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B2");
            _courseJsonDocument  = new JsonPatchDocument<CourseModel>();
            _courses = new List<CourseModel>()
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
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockCourseDao = new Mock<ICourseDao>();
            _sut = null;
            _courseGuid = new Guid();
            _studentGuid = new Guid();
            _courseJsonDocument = null;
            _courses = null;
        }

        [TestMethod]
        public async Task CreateCourse_ReturnsOkResponse_WhenModelIsValid()
        { 
            // Arrange
            _mockCourseDao
                .Setup(x => x.CreateCourse(It.IsAny<CourseModel>()))
                .Callback(() => { return; });

            // Act
            var result = await _sut.CreateCourse(_courses.First());

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var courseInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            courseInApiOkResponse.Should().NotBeNull();
            courseInApiOkResponse.Should().BeEquivalentTo(_courses.First());
        }

        [TestMethod]
        public async Task GetCoursesById_ReturnsCourseAndOkResponse_WhenGuidIsValid()
        {
            // Arrange
            _mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(_courseGuid))
                .ReturnsAsync(_courses.First());

            // Act
            var result = await _sut.GetCourseById(_courseGuid);
            
            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var courseInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            courseInApiOkResponse.Should().NotBeNull();
            courseInApiOkResponse.Should().BeEquivalentTo(_courses.First());
        }

        [TestMethod]
        public async Task GetCourseById_ReturnsNotFoundResponse_WhenGuidIsInvalid()
        {
            // Act
            var result = await _sut.GetCourseById(_courseGuid);
            
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
            _mockCourseDao
                .Setup(x => x.GetCourseByStatus("Active"))
                .ReturnsAsync(_courses);

            // Act
            var result = await _sut.GetCourseByStatus("Active");

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var coursesInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            coursesInApiOkResponse.Should().BeEquivalentTo(_courses);
        }

        [TestMethod]
        public async Task GetCourseByStatus_ReturnsBadRequestResponse_WhenStatusIsNotActiveOrInactive()
        {
            // Act
            var result = await _sut.GetCourseByStatus("Test");

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
            _mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(_courseGuid))
                .ReturnsAsync(_courses.First());

            // Act
            var result = await _sut.PartiallyUpdateCourseById(_courseGuid, _courseJsonDocument);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var courseInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            courseInApiOkResponse.Should().NotBeNull();
            courseInApiOkResponse.Should().BeEquivalentTo(_courses.First());
        }

        [TestMethod]
        public async Task PartiallyUpdateCourseById_ReturnsNotFound_WhenGuidIsInvalid()
        {
            // Act
            var result = await _sut.PartiallyUpdateCourseById(_courseGuid, _courseJsonDocument);

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
            _mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(_courseGuid))
                .ReturnsAsync(_courses.First());

            // Act
            var result = await _sut.DeleteCourseById(_courseGuid);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var courseInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            courseInApiOkResponse.Should().NotBeNull();
            courseInApiOkResponse.Should().BeEquivalentTo(_courses.First());
        }

        [TestMethod]
        public async Task DeleteCourseById_ReturnsNotFoundResponse_WhenCourseGuidIsInvalid()
        {
            // Act
            var result = await _sut.DeleteCourseById(_courseGuid);

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