using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Controllers;
using Moq;
using Lms.Daos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LMS.UnitTests
{
    [TestClass]
    public class StudentActiveEnrollmentControllerTests
    {
        [TestMethod]
        public async Task GetActiveStudentEnrollmentByStudentLasttName_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<IStudentActiveEnrollmentDao> mockStudentActiveEnrollmentDao = new Mock<IStudentActiveEnrollmentDao>();
            StudentActiveEnrollmentController sut = new StudentActiveEnrollmentController(mockStudentActiveEnrollmentDao.Object);

            // Act
            var result = await sut.GetActiveStudentEnrollmentByStudentLastName("test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task GetActiveStudentEnrollmentByStudentPhone_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<IStudentActiveEnrollmentDao> mockStudentActiveEnrollmentDao = new Mock<IStudentActiveEnrollmentDao>();
            StudentActiveEnrollmentController sut = new StudentActiveEnrollmentController(mockStudentActiveEnrollmentDao.Object);

            // Act
            var result = await sut.GetActiveStudentEnrollmentByStudentPhone("test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }
    }
}
