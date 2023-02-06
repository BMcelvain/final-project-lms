using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Controllers;
using Moq;
using Lms.Daos;
using System.Threading.Tasks;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class CourseControllerTests
    {
        [TestMethod]
        public async Task CreateCourse_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            var course = new CourseModel();

            // Act
            var response = await sut.CreateCourse(course);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        //[TestMethod]
        //public async Task CreateClass_Returns500StatusCode()
        //{

        //}
    }
}