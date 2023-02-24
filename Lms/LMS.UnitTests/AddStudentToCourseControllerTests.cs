using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Controllers;
using Moq;
using Lms.Daos;
using System.Threading.Tasks;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.JsonPatch;

namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class AddStudentToCourseControllerTests
    {
        [TestMethod]
        public async Task AddStudentToCourse_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<IAddStudentToCourseDao> mockAddStudentToCourseDao = new Mock<IAddStudentToCourseDao>();
            AddStudentToCourseController sut = new AddStudentToCourseController(mockAddStudentToCourseDao.Object);
            var course = new AddStudentToCourseModel();

            // Act
            var result = await sut.AddStudentToCourse(course);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task AddStudentToCourse_ThrowsException_OnError()
        {
            // Arrange
            Mock<IAddStudentToCourseDao> mockAddStudentToCourseDao = new Mock<IAddStudentToCourseDao>();
            var testException = new Exception("Test Exception");
            var testCourse = new AddStudentToCourseModel();

            mockAddStudentToCourseDao
                .Setup(x => x.AddStudentToCourse(It.IsAny<AddStudentToCourseModel>()))
                .Throws(testException);
            AddStudentToCourseController sut = new AddStudentToCourseController(mockAddStudentToCourseDao.Object);

            // Act
            var result = await sut.AddStudentToCourse(testCourse);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetCourseById_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<IAddStudentToCourseDao> mockAddStudentToCourseDao = new Mock<IAddStudentToCourseDao>();

            mockAddStudentToCourseDao
                .Setup(x => x.GetCourseByCourseId(0))
                .ReturnsAsync(
                new AddStudentToCourseModel()
                {
                    CourseId = 0,
                    SemesterId = 0,
                    StudentId = 0,
                    EnrollmentDate = "11/11/2022",
                    Cancelled = false,
                    CancellationReason = "test",
                    HasPassed = false
                });

            AddStudentToCourseController sut = new AddStudentToCourseController(mockAddStudentToCourseDao.Object);

            // Act
            var result = await sut.GetCourseByCourseId(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetCourseById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IAddStudentToCourseDao> mockAddStudentToCourseDao = new Mock<IAddStudentToCourseDao>();
            AddStudentToCourseController sut = new AddStudentToCourseController(mockAddStudentToCourseDao.Object);
            var testException = new Exception("Test Exception");

            mockAddStudentToCourseDao
                .Setup(x => x.GetCourseByCourseId(0))
                .Throws(testException);

            // Act
            var result = await sut.GetCourseByCourseId(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }



        [TestMethod]
        public async Task PartiallyUpdateCourseById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<IAddStudentToCourseDao> mockAddStudentToCourseDao = new Mock<IAddStudentToCourseDao>();
            mockAddStudentToCourseDao
                .Setup(x => x.GetCourseByCourseId(0))
                .ReturnsAsync(
                new AddStudentToCourseModel()
                {
                    CourseId = 0,
                    SemesterId = 0,
                    StudentId = 0,
                    EnrollmentDate = "11/11/2022",
                    Cancelled = false,
                    CancellationReason = "test",
                    HasPassed = false
                });

            JsonPatchDocument<AddStudentToCourseModel> testDocument = new JsonPatchDocument<AddStudentToCourseModel>();
            AddStudentToCourseController sut = new AddStudentToCourseController(mockAddStudentToCourseDao.Object);

            // Act
            var result = await sut.PartiallyUpdateStudentInCourseByCourseStudentId(0, 0, testDocument);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentInCourseByCourseStudentId_ReturnsNotFound_WhenGetCourseIdReturnsNull()
        {
            // Arrange
            Mock<IAddStudentToCourseDao> mockAddStudentToCourseDao = new Mock<IAddStudentToCourseDao>();
            AddStudentToCourseController sut = new AddStudentToCourseController(mockAddStudentToCourseDao.Object);
            JsonPatchDocument<AddStudentToCourseModel> testDocument = new JsonPatchDocument<AddStudentToCourseModel>();

            // Act
            var result = await sut.PartiallyUpdateStudentInCourseByCourseStudentId(0, 0, testDocument);

            // Arrange
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentInCourseByCourseStudentId_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IAddStudentToCourseDao> mockAddStudentToCourseDao = new Mock<IAddStudentToCourseDao>();
            AddStudentToCourseController sut = new AddStudentToCourseController(mockAddStudentToCourseDao.Object);
            JsonPatchDocument<AddStudentToCourseModel> testDocument = new JsonPatchDocument<AddStudentToCourseModel>();
            var testException = new Exception("Test Exception");

            mockAddStudentToCourseDao
                .Setup(x => x.GetCourseByCourseId(0))
                .Throws(testException);

            // Act
            var result = await sut.PartiallyUpdateStudentInCourseByCourseStudentId(0, 0, testDocument);

            // Arrange
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task DeleteStudentInCourseByStudentCourseId_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<IAddStudentToCourseDao> mockAddStudentToCourseDao = new Mock<IAddStudentToCourseDao>();
            mockAddStudentToCourseDao
                .Setup(x => x.GetCourseByCourseId(0))
                .ReturnsAsync(
                new AddStudentToCourseModel()
                {
                    CourseId = 0,
                    SemesterId = 0,
                    StudentId = 0,
                    EnrollmentDate = "11/11/2022",
                    Cancelled = false,
                    CancellationReason = "test",
                    HasPassed = false
                });
            AddStudentToCourseController sut = new AddStudentToCourseController(mockAddStudentToCourseDao.Object);

            // Act
            var result = await sut.DeleteStudentInCourseByStudentCourseId(0, 0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteCourseById_ReturnsNotFound_WhenGetCourseByIdReturnsNull()
        {
            // Arrange
            Mock<IAddStudentToCourseDao> mockAddStudentToCourseDao = new Mock<IAddStudentToCourseDao>();
            AddStudentToCourseController sut = new AddStudentToCourseController(mockAddStudentToCourseDao.Object);

            // Act
            var result = await sut.DeleteStudentInCourseByStudentCourseId(0, 0);

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteCourseById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IAddStudentToCourseDao> mockAddStudentToCourseDao = new Mock<IAddStudentToCourseDao>();
            AddStudentToCourseController sut = new AddStudentToCourseController(mockAddStudentToCourseDao.Object);
            var testException = new Exception("Test Exception");

            mockAddStudentToCourseDao
                .Setup(x => x.GetCourseByCourseId(0))
                .Throws(testException);

            // Act
            var result = await sut.DeleteStudentInCourseByStudentCourseId(0, 0);

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }
    }
}
