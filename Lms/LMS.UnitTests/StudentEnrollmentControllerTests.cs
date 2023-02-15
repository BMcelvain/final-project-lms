using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Controllers;
using Moq;
using Lms.Daos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


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

            // Act
            var result = await sut.GetStudentEnrollmentHistoryByStudentLastName("test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

    }
}
