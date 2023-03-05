using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Controllers;
using Moq;
using Lms.Daos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lms.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.JsonPatch;

namespace LMS.UnitTests
{
    [TestClass]
    public class StudentEnrollmentControllerTests
    {
        [TestMethod]
        public async Task GetStudentEnrollmentById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);
            var mockStudentEnrollment = new List<StudentEnrollmentModel>();

            mockStudentEnrollmentDao
                .Setup(x => x.GetStudentEnrollmentHistoryById(0))
                .ReturnsAsync(mockStudentEnrollment);

            // Act
            var result = await sut.GetStudentEnrollmentHistoryById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetStudentEnrollmentHistoryById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);
            var testException = new Exception("Test Exception");

            mockStudentEnrollmentDao
                .Setup(x => x.GetStudentEnrollmentHistoryById(0))
                .Throws(testException);

            // Act
            var result = await sut.GetStudentEnrollmentHistoryById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetStudentEnrollmentByStudentLastName_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);
            var mockStudentEnrollment = new List<StudentEnrollmentModel>();

            mockStudentEnrollmentDao
                .Setup(x => x.GetStudentEnrollmentHistoryByStudentLastName("test"))
                .ReturnsAsync(mockStudentEnrollment);

            // Act
            var result = await sut.GetStudentEnrollmentHistoryByStudentLastName("test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetStudentEnrollmentByStudentLastName_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);
            var testException = new Exception("Test Exception");

            mockStudentEnrollmentDao
                .Setup(x => x.GetStudentEnrollmentHistoryByStudentLastName("test"))
                .Throws(testException);

            // Act
            var result = await sut.GetStudentEnrollmentHistoryByStudentLastName("test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetStudentEnrollmentByStudentLasttName_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<IStudentEnrollmentDao> mockStudentActiveEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentActiveEnrollmentDao.Object);

            // Act
            var result = await sut.GetActiveStudentEnrollmentByStudentPhone("test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetStudentEnrollmentByStudentPhone_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IStudentEnrollmentDao> mockStudentActiveEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentActiveEnrollmentDao.Object);

            // Act
            var result = await sut.GetActiveStudentEnrollmentByStudentPhone("test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task AddStudentToCourse_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);
            var course = new StudentEnrollmentModel();

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
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            var testException = new Exception("Test Exception");
            var testCourse = new StudentEnrollmentModel();

            mockStudentEnrollmentDao
                .Setup(x => x.AddStudentToCourse(It.IsAny<StudentEnrollmentModel>()))
                .Throws(testException);
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);

            // Act
            var result = await sut.AddStudentToCourse(testCourse);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        
        [TestMethod]
        public async Task PartiallyUpdateCourseById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            mockStudentEnrollmentDao
                .Setup(x => x.GetCourseByCourseId(0))
                .ReturnsAsync(
                new StudentEnrollmentModel()
                {
                    CourseId = 0,
                    SemesterId = 0,
                    StudentId = 0,
                    EnrollmentDate = "11/11/2022",
                    Cancelled = false,
                    CancellationReason = "test",
                    HasPassed = false
                });

            JsonPatchDocument<StudentEnrollmentModel> testDocument = new JsonPatchDocument<StudentEnrollmentModel>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);

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
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);
            JsonPatchDocument<StudentEnrollmentModel> testDocument = new JsonPatchDocument<StudentEnrollmentModel>();

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
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);
            JsonPatchDocument<StudentEnrollmentModel> testDocument = new JsonPatchDocument<StudentEnrollmentModel>();
            var testException = new Exception("Test Exception");

            mockStudentEnrollmentDao
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
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            mockStudentEnrollmentDao
                .Setup(x => x.GetCourseByCourseId(0))
                .ReturnsAsync(
                new StudentEnrollmentModel()
                {
                    CourseId = 0,
                    SemesterId = 0,
                    StudentId = 0,
                    EnrollmentDate = "11/11/2022",
                    Cancelled = false,
                    CancellationReason = "test",
                    HasPassed = false
                });
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);

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
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);

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
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);
            var testException = new Exception("Test Exception");

            mockStudentEnrollmentDao
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

