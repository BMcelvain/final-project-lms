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
        [TestMethod] // Every method must have this. 
        public void CallDao_ShouldCallDao_ReturnCourse()
        {
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>(); //will work when you're actually in code

            CourseController sut = new CourseController(mockCourseDao.Object);

            sut.CallDao();  //in CallDao throw exception 

            mockCourseDao.Verify(courseDao => courseDao.GetCourses(), Times.Once()); //this is used as a temp object
        }

        [TestMethod]
        public async Task CreateClass_ReturnsOkStatusCode()
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
    }
}