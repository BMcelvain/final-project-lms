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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.UnitTests
{
    #nullable disable warnings
    [TestClass]
    public class StudentControllerTests
    {
        private Mock<IStudentDao> _mockStudentDao;
        private StudentController _sut;
        private Guid _studentGuid;
        private Guid _invalidStudentGuid;
        private JsonPatchDocument<StudentModel> _studentJsonDocument;
        private List<StudentModel> _students;
        private IMemoryCache _cache;

        [TestInitialize]
        public void Initialize()
        {
            _mockStudentDao = new Mock<IStudentDao>();
            _cache = new MemoryCache(new MemoryCacheOptions());
            _sut = new StudentController(_mockStudentDao.Object, _cache);
            _studentGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9");
            _invalidStudentGuid = new Guid("00000000-0000-0000-0000-000000000000");
            _studentJsonDocument = new JsonPatchDocument<StudentModel>();
            _students = new List<StudentModel>()
            {
                new StudentModel()
                {
                    StudentId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9"),
                    StudentFirstName = "Fred",
                    StudentLastName = "Testing",
                    StudentPhone = "999-999-9999",
                    StudentEmail = "Test@test.com",
                    StudentStatus = "Test Status"
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockStudentDao = null;
            _sut = null;
            _studentGuid = new Guid();
            _studentJsonDocument = null;
            _students = null;
        }

        [TestMethod]
        public async Task CreateStudent_WhenModelIsValid_ReturnsObjectResult()
        {
            // Arrange
            _mockStudentDao
                .Setup(x => x.CreateStudent(It.IsAny<StudentModel>()))
                .Callback(() => { return; });

            // Act
            var result = await _sut.CreateStudent(_students.First());

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var courseInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            courseInApiOkResponse.Should().NotBeNull();
            courseInApiOkResponse.Should().BeEquivalentTo(_students.First());
        }

        [TestMethod]
        public async Task GetStudentById_ReturnsStudentAndOkResponse_WhenGuidIsValid()
        {
            // Arrange
            _mockStudentDao
                .Setup(x => x.GetStudentById<StudentModel>(_studentGuid))
                .ReturnsAsync(_students.First());

            // Act
            var result = await _sut.GetStudentById(_studentGuid);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.Value.Should().NotBeNull(); 

            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse; 
            apiOkResponseInOkResult.Result.Should().NotBeNull();

            var studentInApiOkResponse = apiOkResponseInOkResult.Result as StudentModel;
            studentInApiOkResponse.Should().NotBeNull(); 
            studentInApiOkResponse.Should().BeEquivalentTo(_students.First()); 
        }

        [TestMethod]
        public async Task GetStudentById_InvalidGuidId_ReturnsNotFoundResponse()
        {
            // Act
            var result = await _sut.GetStudentById(_studentGuid);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Student with that id not found.");
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentById_WithValidStudentIdAndUpdates_ReturnsOkObjectResult()
        {
            // Arrange
            _mockStudentDao
                 .Setup(x => x.GetStudentById<StudentModel>(_studentGuid))
                 .ReturnsAsync(_students.First());
            // Act
            var result = await _sut.PartiallyUpdateStudentById(_studentGuid, _studentJsonDocument);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var studentInApiOkResponse = apiOkResponseInOkResult.Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            studentInApiOkResponse.Should().NotBeNull();
            studentInApiOkResponse.Should().BeEquivalentTo(_students.First());
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentById_ReturnsInvalidResponse_WhenGuidIsInvalid()
        {
            // Act
            var result = await _sut.PartiallyUpdateStudentById(_studentGuid, _studentJsonDocument);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Student with that id not found.");
        }

        [TestMethod]
        public async Task DeleteStudentByValidId_ReturnsOKStatusCode()
        {
            // Arrange
            _mockStudentDao
                .Setup(x => x.GetStudentById<StudentModel>(_studentGuid))
                .ReturnsAsync(_students.First());

            // Act
            var result = await _sut.DeleteStudentById(_studentGuid);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var studentInApiOkResponse = apiOkResponseInOkResult.Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            studentInApiOkResponse.Should().NotBeNull();
            studentInApiOkResponse.Should().BeEquivalentTo(_students.First());
        }

        [TestMethod]
        public async Task DeleteStudentById_ReturnsNotFoundObject_WhenGuidIsInvalid()
        {
            // Act
            var result = await _sut.DeleteStudentById(_invalidStudentGuid);

            // Assert 
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Student with that id not found.");
        }
    }
}