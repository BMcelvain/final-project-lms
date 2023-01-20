using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Controllers;
using Moq;
using Lms.Daos;

namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class CourseControllerTests
    {
        [TestMethod] // Every method must have this. 
        public void CallDao()
        {
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();

            CourseController sut = new CourseController(mockCourseDao.Object);

            sut.CallDao();

            mockCourseDao.Verify(courseDao => courseDao.GetCourse(), Times.Once());

        }
    }
}