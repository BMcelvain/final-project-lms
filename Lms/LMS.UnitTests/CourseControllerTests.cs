using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Controllers;
using Moq;
using Lms.Daos;
using System.Threading.Tasks;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class CourseControllerTests
    {
        [TestMethod] // Every method must have this. 
        public void CallDao_ShouldCallDao_ReturnCourse()
        {
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>(); //will work when you're actually in code

            CourseController sut = new CourseController(mockCourseDao.Object);

            sut.CallDao();  //in CallDao throw exception 

            mockCourseDao.Verify(courseDao => courseDao.GetCourses(), Times.Once()); //this is used as a temp object
        }

        [TestMethod]
        public async Task CreateCourse_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            var course = new CourseModel();

            // Act
            var result = await sut.CreateCourse(course);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task CreateCourse_ThrowsException_OnError()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            var testException = new Exception("Test Exception");
            var testCourse = new CourseModel();

            mockCourseDao
                .Setup(x => x.CreateCourse(It.IsAny<CourseModel>()))
                .Throws(testException);
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.CreateCourse(testCourse);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetCourses_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.GetCourses();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetCoursesById_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();

            mockCourseDao
                .Setup(x => x.GetCourseById(0))
                .ReturnsAsync(
                new CourseModel()
                {
                    CourseId = 0,
                    TeacherId = 0,
                    CourseName = "Test",
                    SemesterId = 0,
                    StartDate = "11/11/2022",
                    EndDate = "12/12/2022",
                    CourseStatus = "Test"
                });

            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.GetCourseById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetCouseByStatus_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.GetCourseByStatus("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateCourseById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            mockCourseDao
                .Setup(x => x.GetCourseById(0))
                .ReturnsAsync(
                new CourseModel()
                {
                    CourseId = 0,
                    TeacherId = 0,
                    CourseName = "Test",
                    SemesterId = 0,
                    StartDate = "11/11/2022",
                    EndDate = "12/12/2022",
                    CourseStatus = "Test"
                });

            JsonPatchDocument<CourseModel> testDocument = new JsonPatchDocument<CourseModel>();    
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.PartiallyUpdateCourseById(0, testDocument);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteCourseById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            mockCourseDao
                .Setup(x => x.GetCourseById(0))
                .ReturnsAsync(
                new CourseModel()
                {
                    CourseId = 0,
                    TeacherId = 0,
                    CourseName = "Test",
                    SemesterId = 0,
                    StartDate = "11/11/2022",
                    EndDate = "12/12/2022",
                    CourseStatus = "Test"
                });
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.DeleteCourseById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}