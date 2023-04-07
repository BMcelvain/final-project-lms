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

        //private static Mock<StudentModel> GetMockStudent()
        //{
        //    var mockStudent = new Mock<StudentModel>();
        //    mockStudent.Object.StudentId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9");
        //    mockStudent.Object.StudentFirstName = "Fred";
        //    mockStudent.Object.StudentLastName = "Testing";
        //    mockStudent.Object.StudentPhone = "999-999-9999";
        //    mockStudent.Object.StudentEmail = "Test@test.com";
        //    mockStudent.Object.StudentStatus = "Test Status";
        //    mockStudent.Object.TotalPassCourses = 3;
        //    return mockStudent;
        //}

        [TestMethod]
        public async Task CreateStudent_ValidStudent_ReturnsOk()
        {

            // Act
            var result = await sut.CreateStudent(students.First());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public async Task GetStudentById_ValidGuidId_ReturnsOkResponse()
        {
            // Arrange
            mockStudentDao
                .Setup(x => x.GetStudentById(studentGuid))
                .ReturnsAsync(students.First());

            // Act
            var result = await sut.GetStudentById(studentGuid);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var studentInApiOkResponse = apiOkResponseInOkResult.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
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
                 .Setup(x => x.GetStudentById(studentGuid))
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
            //// Arrange
            //var invalidId = new Guid("00000000-0000-0000-0000-000000000000");
            //mockStudentDao
            //     .Setup(x => x.PartiallyUpdateStudentById(invalidId,studentJsonDocument))
            //     .Callback(() => { return; });
            //var student = new StudentModel { StudentId = invalidId, StudentFirstName = "Harry" };
            //var patchDoc = new JsonPatchDocument<StudentModel>();
            //patchDoc.Replace(s => s.StudentFirstName, "Joey");
            //mockStudentDao.Setup(x => x.GetStudentById(studentGuid)).ReturnsAsync(student);
            //mockStudentDao.Setup(x => x.PartiallyUpdateStudentById(student)).Returns(Task.CompletedTask);

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
                .Setup(x => x.GetStudentById(studentGuid))
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




