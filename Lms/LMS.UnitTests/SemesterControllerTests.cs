using Lms.Controllers;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace LMS.UnitTests
{
    [TestClass]
    public class SemesterControllerTests
    {
        [TestMethod]
        public async Task CreateSemester_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ISemesterDao> mockSemesterDao = new Mock<ISemesterDao>();
            SemesterController sut = new SemesterController(mockSemesterDao.Object);
            var semester = new SemesterModel();

            // Act
            var result = await sut.CreateSemester(semester);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task CreateSemester_ThrowsException_OnError()
        {
            // Arrange
            Mock<ISemesterDao> mockSemesterDao = new Mock<ISemesterDao>();
            var testException = new Exception("Test Exception");
            var testSemester = new SemesterModel();

            mockSemesterDao
                .Setup(x => x.CreateSemester(It.IsAny<SemesterModel>()))
                .Throws(testException);
            SemesterController sut = new SemesterController(mockSemesterDao.Object);

            // Act
            var result = await sut.CreateSemester(testSemester);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetSemesters_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ISemesterDao> mockSemesterDao = new Mock<ISemesterDao>();
            SemesterController sut = new SemesterController(mockSemesterDao.Object);

            // Act
            var result = await sut.GetSemesters();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetSemesters_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ISemesterDao> mockSemesterDao = new Mock<ISemesterDao>();
            var testException = new Exception("Test Exception");
            SemesterController sut = new SemesterController(mockSemesterDao.Object);

            mockSemesterDao
                .Setup(x => x.GetSemesters())
                .Throws(testException);

            // Act
            var result = await sut.GetSemesters();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetSemestersById_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ISemesterDao> mockSemesterDao = new Mock<ISemesterDao>();

            mockSemesterDao
                .Setup(x => x.GetSemesterById(0))
                .ReturnsAsync(
                new SemesterModel()
                {
                    Semester = "Test",
                    Year = "0000"
                });

            SemesterController sut = new SemesterController(mockSemesterDao.Object);

            // Act
            var result = await sut.GetSemesterById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetSemesterById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ISemesterDao> mockSemesterDao = new Mock<ISemesterDao>();
            SemesterController sut = new SemesterController(mockSemesterDao.Object);
            var testException = new Exception("Test Exception");

            mockSemesterDao
                .Setup(x => x.GetSemesterById(0))
                .Throws(testException);

            // Act
            var result = await sut.GetSemesterById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task DeleteSemesterById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ISemesterDao> mockSemesterDao = new Mock<ISemesterDao>();
            mockSemesterDao
                .Setup(x => x.GetSemesterById(0))
                .ReturnsAsync(
                new SemesterModel()
                {
                    Semester = "Test",
                    Year = "0000"
                });
            SemesterController sut = new SemesterController(mockSemesterDao.Object);

            // Act
            var result = await sut.DeleteSemesterById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteSemesterById_ReturnsNotFound_WhenGetSemesterByIdReturnsNull()
        {
            // Arrange
            Mock<ISemesterDao> mockSemesterDao = new Mock<ISemesterDao>();
            SemesterController sut = new SemesterController(mockSemesterDao.Object);

            // Act
            var result = await sut.DeleteSemesterById(0);

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteSemesterById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ISemesterDao> mockSemesterDao = new Mock<ISemesterDao>();
            SemesterController sut = new SemesterController(mockSemesterDao.Object);
            var testException = new Exception("Test Exception");

            mockSemesterDao
                .Setup(x => x.GetSemesterById(0))
                .Throws(testException);

            // Act
            var result = await sut.DeleteSemesterById(0);

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }
    }
}
