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
    public class StudentControllerTests
    {
        Mock<IStudentDao> mockStudentDao;
        StudentController sut;
        Guid studentGuid;
        Guid invalidStudentGuid;
        JsonPatchDocument<StudentModel> studentJsonDocument;
        List<StudentModel> students;


        [TestInitialize]
        public void Initialize()
        {
            mockStudentDao = new Mock<IStudentDao>();
            sut = new StudentController(mockStudentDao.Object);
            studentGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9");
            invalidStudentGuid = new Guid("00000000-0000-0000-0000-000000000000");
            studentJsonDocument = new JsonPatchDocument<StudentModel>();
            students = new List<StudentModel>()
            {
                new StudentModel()
                {
                    StudentId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9"),
                    StudentFirstName = "Fred",
                    StudentLastName = "Testing",
                    StudentPhone = "999-999-9999",
                    StudentEmail = "Test@test.com",
                    StudentStatus = "Test Status",
                    TotalPassCourses = 3
                }
            };
    
        }

        [TestCleanup]
        public void Cleanup()
        {
            mockStudentDao = null;
            sut = null;
            studentGuid = new Guid();
            studentJsonDocument = null;
            students = null;
        }

        [TestMethod]
        public async Task CreateStudent_WhenModelIsValid_ReturnsObjectResult()
        {

            // Arrange
            mockStudentDao
                .Setup(x => x.CreateStudent(It.IsAny<StudentModel>()))
                .Callback(() => { return; });

            // Act
            var result = await sut.CreateStudent(students.First());

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var courseInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            courseInApiOkResponse.Should().NotBeNull();
            courseInApiOkResponse.Should().BeEquivalentTo(students.First());
        }

        [TestMethod]
        public async Task GetStudentById_ReturnsStudentAndOkResponse_WhenGuidIsValid()
        {
            // Arrange
            mockStudentDao
                .Setup(x => x.GetStudentById<StudentModel>(studentGuid))
                .ReturnsAsync(students.First());

            // Act
            var result = await sut.GetStudentById(studentGuid);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();

            var okResult = result as ObjectResult;
            okResult.Value.Should().NotBeNull(); 

            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse; 
            apiOkResponseInOkResult.Result.Should().NotBeNull(); // 

            var studentInApiOkResponse = apiOkResponseInOkResult.Result as StudentModel;
            studentInApiOkResponse.Should().NotBeNull(); 
            studentInApiOkResponse.Should().BeEquivalentTo(students.First()); 
        }

        [TestMethod]
        public async Task GetStudentById_InvalidGuidId_ReturnsNotFoundResponse()
        {

            // Act
            var result = await sut.GetStudentById(studentGuid);

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
            mockStudentDao
                 .Setup(x => x.GetStudentById<StudentModel>(studentGuid))
                 .ReturnsAsync(students.First());
            // Act
            var result = await sut.PartiallyUpdateStudentById(studentGuid, studentJsonDocument);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var studentInApiOkResponse = apiOkResponseInOkResult.Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            studentInApiOkResponse.Should().NotBeNull();
            studentInApiOkResponse.Should().BeEquivalentTo(students.First());
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentById_ReturnsInvalidResponse_WhenGuidIsInvalid()
        {
            // Act
            var result = await sut.PartiallyUpdateStudentById(studentGuid, studentJsonDocument);

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
            mockStudentDao
                .Setup(x => x.GetStudentById<StudentModel>(studentGuid))
                .ReturnsAsync(students.First());

            // Act
            var result = await sut.DeleteStudentById(studentGuid);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var studentInApiOkResponse = apiOkResponseInOkResult.Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            studentInApiOkResponse.Should().NotBeNull();
            studentInApiOkResponse.Should().BeEquivalentTo(students.First());
        }
    

        [TestMethod]
        public async Task DeleteStudentById_ReturnsNotFoundObject_WhenGuidIsInvalid()
        {
            // Act
            var result = await sut.DeleteStudentById(invalidStudentGuid);

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




