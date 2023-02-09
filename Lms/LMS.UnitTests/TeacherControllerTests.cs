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

namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class TeacherControllerTests
    {
        [TestMethod] // Every method must have this. 
        public void CallTeacherDao()
        {
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>(); //will work when you're actually in code

            TeacherController sut = new TeacherController(mockTeacherDao.Object);

            _ = sut.GetTeachers();  //in CallDao throw exception 

            mockTeacherDao.Verify(teacherDao => teacherDao.GetTeachers(), Times.Once()); //this is used as a temp object
        }

        [TestMethod]
        public async Task CreateClass_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            TeacherController sut = new TeacherController(mockTeacherDao.Object);
            var teacher = new TeacherModel();

            // Act
            var response = await sut.CreateTeacher(teacher);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public async Task CreateTeacher_ThrowsException_OnError()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            var testException = new Exception("Test Exception");
            var testTeacher = new TeacherModel();

            mockTeacherDao
                .Setup(x => x.CreateTeacher(It.IsAny<TeacherModel>()))
                .Throws(testException);
            TeacherController sut = new TeacherController(mockTeacherDao.Object);

            // Act
            var result = await sut.CreateTeacher(testTeacher);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetTeachers_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            TeacherController sut = new TeacherController(mockTeacherDao.Object);

            // Act
            var result = await sut.GetTeachers();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetTeachers_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            var testException = new Exception("Test Exception");
            TeacherController sut = new TeacherController(mockTeacherDao.Object);

            mockTeacherDao
                .Setup(x => x.GetTeachers())
                .Throws(testException);

            // Act
            var result = await sut.GetTeachers();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetTeachersById_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();

            mockTeacherDao
                .Setup(x => x.GetTeacherById(0))
                .ReturnsAsync(
                new TeacherModel()
                {
                    TeacherId = 0,
                    TeacherFirstName = "Test",
                    TeacherLastName = "Test",
                    TeacherPhone = "999-99-9999",
                    TeacherEmail = "test@test.com",
                    TeacherStatus = "Test"
                });

            TeacherController sut = new TeacherController(mockTeacherDao.Object);

            // Act
            var result = await sut.GetTeacherById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetTeacherById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            TeacherController sut = new TeacherController(mockTeacherDao.Object);
            var testException = new Exception("Test Exception");

            mockTeacherDao
                .Setup(x => x.GetTeacherById(0))
                .Throws(testException);

            // Act
            var result = await sut.GetTeacherById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetTeacherByStatus_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            TeacherController sut = new TeacherController(mockTeacherDao.Object);

            // Act
            var result = await sut.GetTeacherByStatus("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetTeacherByStatus_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            TeacherController sut = new TeacherController(mockTeacherDao.Object);
            var testException = new Exception("Test Exception");

            mockTeacherDao
                .Setup(x => x.GetTeacherByStatus("Test"))
                .Throws(testException);

            // Act
            var result = await sut.GetTeacherByStatus("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateTeacherById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            mockTeacherDao
                .Setup(x => x.GetTeacherById(0))
                .ReturnsAsync(
                new TeacherModel()
                {
                    TeacherId = 0,
                    TeacherFirstName = "Test",
                    TeacherLastName = "Test",
                    TeacherPhone = "999-99-9999",
                    TeacherEmail = "test@test.com",
                    TeacherStatus = "Test"
                });

            JsonPatchDocument<TeacherModel> testDocument = new JsonPatchDocument<TeacherModel>();
            TeacherController sut = new TeacherController(mockTeacherDao.Object);

            // Act
            var result = await sut.PartiallyUpdateTeacherById(0, testDocument);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateTeacherById_ReturnsNotFound_WhenGetTeacherIdReturnsNull()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            TeacherController sut = new TeacherController(mockTeacherDao.Object);
            JsonPatchDocument<TeacherModel> testDocument = new JsonPatchDocument<TeacherModel>();

            // Act
            var result = await sut.PartiallyUpdateTeacherById(0, testDocument);

            // Arrange
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateTeacherById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            TeacherController sut = new TeacherController(mockTeacherDao.Object);
            JsonPatchDocument<TeacherModel> testDocument = new JsonPatchDocument<TeacherModel>();
            var testException = new Exception("Test Exception");

            mockTeacherDao
                .Setup(x => x.GetTeacherById(0))
                .Throws(testException);

            // Act
            var result = await sut.PartiallyUpdateTeacherById(0, testDocument);

            // Arrange
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task DeleteTeacherById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            mockTeacherDao
                .Setup(x => x.GetTeacherById(0))
                .ReturnsAsync(
                new TeacherModel()
                {
                    TeacherId = 0,
                    TeacherFirstName = "Test",
                    TeacherLastName = "Test",
                    TeacherPhone = "999-99-9999",
                    TeacherEmail = "test@test.com",
                    TeacherStatus = "Test"
                });
            TeacherController sut = new TeacherController(mockTeacherDao.Object);

            // Act
            var result = await sut.DeleteTeacherById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteTeacherById_ReturnsNotFound_WhenGetTeacherByIdReturnsNull()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            TeacherController sut = new TeacherController(mockTeacherDao.Object);

            // Act
            var result = await sut.DeleteTeacherById(0);

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteTeacherById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            TeacherController sut = new TeacherController(mockTeacherDao.Object);
            var testException = new Exception("Test Exception");

            mockTeacherDao
                .Setup(x => x.GetTeacherById(0))
                .Throws(testException);

            // Act
            var result = await sut.DeleteTeacherById(0);

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }
    }
}

