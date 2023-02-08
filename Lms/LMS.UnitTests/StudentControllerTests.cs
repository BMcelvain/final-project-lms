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
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Configuration.EnvironmentVariables;


namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class StudentControllerTests
    {
        [TestMethod] // Every method must have this. 
        public void CallDao()
        {
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>(); //will work when you're actually in code

            StudentController sut = new StudentController(mockStudentDao.Object);

            _ = sut.GetStudents();  //in CallDao throw exception 

            mockStudentDao.Verify(studentDao => studentDao.GetStudents(), Times.Once()); //this is used as a temp object
        }

        [TestMethod]
        public async Task CreateStudent_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            StudentController sut = new StudentController(mockStudentDao.Object);
            var student = new StudentModel();

            // Act
            var response = await sut.CreateStudent(student);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public async Task CreateStudent_ThrowsException_OnError()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            var testException = new Exception("Test Exception");
            var testStudent = new StudentModel();

            mockStudentDao
                .Setup(x => x.CreateStudent(It.IsAny<StudentModel>()))
                .Throws(testException);
            StudentController sut = new StudentController(mockStudentDao.Object);

            // Act
            var result = await sut.CreateStudent(testStudent);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetStudents_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            StudentController sut = new StudentController(mockStudentDao.Object);

            // Act
            var result = await sut.GetStudents();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetStudents_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            var testException = new Exception("Test Exception");
            StudentController sut = new StudentController(mockStudentDao.Object);

            mockStudentDao
                .Setup(x => x.GetStudents())
                .Throws(testException);

            // Act
            var result = await sut.GetStudents();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetStudentsById_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();

            mockStudentDao
                .Setup(x => x.GetStudentById(0))
                .ReturnsAsync(
                new StudentModel()
                {
                    StudentId = 0,
                    StudentFirstName = "Test",
                    StudentLastName = "Test",
                    StudentPhone = "999-999-9999",
                    StudentEmail = "Test@test.com",
                    StudentStatus = "Test Status",
                    TotalPassCourses = 0
                });

            StudentController sut = new StudentController(mockStudentDao.Object);

            // Act
            var result = await sut.GetStudentById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetStudentById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            StudentController sut = new StudentController(mockStudentDao.Object);
            var testException = new Exception("Test Exception");

            mockStudentDao
                .Setup(x => x.GetStudentById(0))
                .Throws(testException);

            // Act
            var result = await sut.GetStudentById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            mockStudentDao
                .Setup(x => x.GetStudentById(0))
                .ReturnsAsync(
                new StudentModel()
                {
                    StudentId = 0,
                    StudentFirstName = "Test",
                    StudentLastName = "Test",
                    StudentPhone = "999-999-9999",
                    StudentEmail = "Test@test.com",
                    StudentStatus = "Test Status",
                    TotalPassCourses = 0
                });

            JsonPatchDocument<StudentModel> testDocument = new JsonPatchDocument<StudentModel>();
            StudentController sut = new StudentController(mockStudentDao.Object);

            // Act
            var result = await sut.PartiallyUpdateStudentById(0, testDocument);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentById_ReturnsNotFound_WhenGetStudentIdReturnsNull()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            StudentController sut = new StudentController(mockStudentDao.Object);
            JsonPatchDocument<StudentModel> testDocument = new JsonPatchDocument<StudentModel>();

            // Act
            var result = await sut.PartiallyUpdateStudentById(0, testDocument);

            // Arrange
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            StudentController sut = new StudentController(mockStudentDao.Object);
            JsonPatchDocument<StudentModel> testDocument = new JsonPatchDocument<StudentModel>();
            var testException = new Exception("Test Exception");

            mockStudentDao
                .Setup(x => x.GetStudentById(0))
                .Throws(testException);

            // Act
            var result = await sut.PartiallyUpdateStudentById(0, testDocument);

            // Arrange
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task DeleteStudentById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            mockStudentDao
                .Setup(x => x.GetStudentById(0))
                .ReturnsAsync(
                new StudentModel()
                {
                    StudentId = 0,
                    StudentFirstName = "Test",
                    StudentLastName = "Test",
                    StudentPhone = "999-999-9999",
                    StudentEmail = "Test@test.com",
                    StudentStatus = "Test Status",
                    TotalPassCourses = 0
                });
            StudentController sut = new StudentController(mockStudentDao.Object);

            // Act
            var result = await sut.DeleteStudentById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteStudentById_ReturnsNotFound_WhenGetStudentByIdReturnsNull()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            StudentController sut = new StudentController(mockStudentDao.Object);

            // Act
            var result = await sut.DeleteStudentById(0);

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteStudentById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            StudentController sut = new StudentController(mockStudentDao.Object);
            var testException = new Exception("Test Exception");

            mockStudentDao
                .Setup(x => x.GetStudentById(0))
                .Throws(testException);

            // Act
            var result = await sut.DeleteStudentById(0);

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }
    }
}


