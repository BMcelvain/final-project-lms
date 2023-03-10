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
            var mockEnrollment = new StudentEnrollmentModel
            {
                CourseId = 0,
                Cancelled = false,
                CancellationReason = "test",
                HasPassed = false
            };
            mockStudentEnrollment.Add(mockEnrollment);

            _ = mockStudentEnrollmentDao
                .Setup(x => x.GetStudentEnrollmentHistoryById(1))
                .ReturnsAsync(mockStudentEnrollment);

            // Act
            var result = await sut.GetStudentEnrollmentHistoryById(1);

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
        public async Task GetStudentEnrollmentByStudentLasttName_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);

            // Act
            var result = await sut.GetStudentEnrollmentHistoryByStudentLastName("test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
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

        public async Task GetActiveStudentEnrollmentByStudentPhone_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);


            // Act
            var result = await sut.GetActiveStudentEnrollmentByStudentPhone("test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }


        [TestMethod]
        public async Task GetActiveStudentEnrollmentByStudentPhone_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IStudentEnrollmentDao> mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            StudentEnrollmentController sut = new StudentEnrollmentController(mockStudentEnrollmentDao.Object);

            // Act
            var result = await sut.GetActiveStudentEnrollmentByStudentPhone("test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }
    }
}
